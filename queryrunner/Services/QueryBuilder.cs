using System;

namespace queryrunner.Services
{
    public static class QueryBuilder
    {
        public static string BuildWHPQuery(string whereClause)
        {
            return $@"
            WITH one AS (
                SELECT
                    well_name,
                    'DIGDAT\WellHistoryProfile\Word\' || WHP_DOC_FILE_NAME AS pathfile,
                    WHP_CREATED_BY AS loadBy,
                    TO_CHAR(WHP_CREATED_DATE, 'dd-MON-yyyy') AS loadDate,
                    lookup.get_aset_name(lookup.get_structure_name(well_name)) || '\' ||
                    lookup.get_structure_name(well_name) || '\' || well_name || '\' || 'WHP' AS FOLDER,
                    NULL AS FROM_TAB,
                    NULL AS TAB_ROWID
                FROM
                    well_history_profile a
                WHERE
                    WHP_LAST_UPDATE = (
                        SELECT MAX(WHP_LAST_UPDATE)
                        FROM WELL_HISTORY_PROFILE
                        WHERE well_name = a.well_name
                    )
                    AND WHP_DOC_FILE_NAME IS NOT NULL
                    AND {whereClause}

                UNION ALL

                SELECT
                    well_name,
                    'DIGDAT\WellHistoryProfile\Excel\' || WHP_WS_FILE_NAME AS pathfile,
                    WHP_CREATED_BY AS loadBy,
                    TO_CHAR(WHP_CREATED_DATE, 'dd-MON-yyyy') AS loadDate,
                    lookup.get_aset_name(lookup.get_structure_name(well_name)) || '\' ||
                    lookup.get_structure_name(well_name) || '\' || well_name || '\' || 'WHP' AS FOLDER,
                    NULL AS FROM_TAB,
                    NULL AS TAB_ROWID
                FROM
                    well_history_profile a
                WHERE
                    WHP_LAST_UPDATE = (
                        SELECT MAX(WHP_LAST_UPDATE)
                        FROM WELL_HISTORY_PROFILE
                        WHERE well_name = a.well_name
                    )
                    AND WHP_WS_FILE_NAME IS NOT NULL
                    AND {whereClause}
            )
            SELECT * FROM one ORDER BY 1, 2";
        }

        public static string BuildHKPQuery()
        {
            return @"
                WITH one AS (
                    SELECT 
                        b.w_location well_name,
                        REPLACE(a.haf_file_path,'/','\') || '\' || a.haf_file_name AS pathfile,
                        a.haf_load_by AS loadBy,
                        TO_CHAR(a.haf_load_date,'dd-MON-yyyy') loadDate,
                        'HKP\'||HKP_ASSET_FLAG_DESC||'\'||REPLACE(REPLACE(b.w_location,'/','_'),' ','')||'\'||DECODE(HAF_TYPE,'0','DOKUMEN','1','PETA',HAF_TYPE)||'\' AS FOLDER,
                        NULL AS FROM_TAB,
                        NULL AS TAB_ROWID
                    FROM hkp_asset_files a
                    JOIN hkp_asset b ON a.ha_s = b.ha_s
                    JOIN hkp_asset_flag c ON b.HA_ASSET_FLAG = c.HKP_ASSET_FLAG
                    WHERE b.W_LOCATION IN (
                        'EMP.SP-III.BNG','BNG-DL-3','BNG-HH-1','BNG-V','BNG-DL-8','BNG-BBL-6'
                    )

                    UNION ALL

                    SELECT 
                        b.w_location well_name,
                        REPLACE(a.haod_file_path,'/','\') || '\' || a.haod_file_name AS pathfile,
                        a.haod_loaded_by AS loadBy,
                        TO_CHAR(a.haod_loaded_date,'dd-MON-yyyy') loadDate,
                        'HKP\'||HKP_ASSET_FLAG_DESC||'\'||REPLACE(REPLACE(b.w_location,'/','_'),' ','_')||'\' AS FOLDER,
                        NULL AS FROM_TAB,
                        NULL AS TAB_ROWID
                    FROM hkp_asset_owner_detail a
                    JOIN hkp_asset b ON a.ha_s = b.ha_s
                    JOIN hkp_asset_flag c ON b.HA_ASSET_FLAG = c.HKP_ASSET_FLAG
                    WHERE b.W_LOCATION IN (
                        'EMP.SP-III.BNG','BNG-DL-3','BNG-HH-1','BNG-V','BNG-DL-8','BNG-BBL-6'
                    )
                )
                SELECT well_name, REPLACE(UPPER(pathfile),'\DIGDAT','DIGDAT') pathfile, loadby, loaddate, FOLDER, FROM_TAB, TAB_ROWID 
                FROM one 
                ORDER BY 1, 2";
        }

        public static string BuildWhereClause(string type, string value)
        {
            switch (type)
            {
                case "Well Name Like":
                    return $"well_name LIKE '{value}'";
                case "Struc Name =":
                    return $"lookup.get_structure_name(well_name) = '{value}'";
                case "Struc In Asset":
                    return $@"
                        well_name IN (
                            SELECT well_name
                            FROM well
                            WHERE structure_s IN (
                                SELECT structure_s
                                FROM structure
                                WHERE aset_id IN {value}
                            )
                        )";
                case "File Name Like":
                    return $"(WHP_DOC_FILE_NAME LIKE '{value}' OR WHP_WS_FILE_NAME LIKE '{value}')";
                case "Well Name In":
                    return $"well_name IN {value}";
                case "Struc Name In":
                    return $"lookup.get_structure_name(well_name) IN {value}";
                default:
                    return "1=1";
            }
        }
    }
}
