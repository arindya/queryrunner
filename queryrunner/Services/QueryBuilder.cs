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
        public static string BuildWELLLOGQuery(string whereClause, string type)
        {
            string clause1 = ""; // untuk tiap blok
            string clause2 = "";

            if (type == "File Name Like")
            {
                // langsung LIKE ke masing-masing kolom
                clause1 = $"UPPER(wld_file_name) LIKE '{whereClause}'";
                clause2 = $"UPPER(wli_file_name) LIKE '{whereClause}'";
                string clause3 = $"UPPER(wli_hdr_file_name) LIKE '{whereClause}'";
                string clause4 = $"UPPER(wml_file_name) LIKE '{whereClause}'";

                return $@"
                    WITH one AS (
                        SELECT well_name, wld_file_path || '\' || wld_file_name AS pathfile,
                               wld_load_by AS loadBy, TO_CHAR(wld_load_date,'dd-MON-yyyy') loadDate,
                               NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                        FROM well_log, well_log_data
                        WHERE well_log.well_log_s = well_log_data.well_log_s
                          AND {clause1}

                        UNION ALL

                        SELECT well_name, wli_file_path || '\' || wli_file_name AS pathfile,
                               wli_load_by AS loadBy, TO_CHAR(wli_load_date,'dd-MON-yyyy') loadDate,
                               NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                        FROM well_log, well_log_image
                        WHERE well_log.well_log_s = well_log_image.well_log_s
                          AND wli_file_name IS NOT NULL
                          AND {clause2}

                        UNION ALL

                        SELECT well_name, wli_HDR_file_path || '\' || wli_hdr_file_name AS pathfile,
                               wli_load_by AS loadBy, TO_CHAR(wli_load_date,'dd-MON-yyyy') loadDate,
                               NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                        FROM well_log, well_log_image
                        WHERE well_log.well_log_s = well_log_image.well_log_s
                          AND wli_hdr_file_name IS NOT NULL
                          AND {clause3}

                        UNION ALL

                        SELECT well_name, wml_file_path || '\' || wml_file_name AS pathfile,
                               wml_load_by AS loadBy, TO_CHAR(wml_load_date,'dd-MON-yyyy') loadDate,
                               NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                        FROM well_master_log
                        WHERE {clause4}
                    )
                    SELECT * FROM one ORDER BY 1, 2";
            }
            else
            {                    
                return $@"
                    WITH one AS (
                    SELECT well_name, wld_file_path || '\' || wld_file_name AS pathfile,
                           wld_load_by AS loadBy, TO_CHAR(wld_load_date,'dd-MON-yyyy') loadDate,
                           NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                    FROM well_log, well_log_data
                    WHERE well_log.well_log_s = well_log_data.well_log_s
                      AND {whereClause}

                    UNION ALL

                    SELECT well_name, wli_file_path || '\' || wli_file_name AS pathfile,
                           wli_load_by AS loadBy, TO_CHAR(wli_load_date,'dd-MON-yyyy') loadDate,
                           NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                    FROM well_log, well_log_image
                    WHERE well_log.well_log_s = well_log_image.well_log_s
                      AND wli_file_name IS NOT NULL
                      AND {whereClause}

                    UNION ALL

                    SELECT well_name, wli_HDR_file_path || '\' || wli_hdr_file_name AS pathfile,
                           wli_load_by AS loadBy, TO_CHAR(wli_load_date,'dd-MON-yyyy') loadDate,
                           NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                    FROM well_log, well_log_image
                    WHERE well_log.well_log_s = well_log_image.well_log_s
                      AND wli_hdr_file_name IS NOT NULL
                      AND {whereClause}

                    UNION ALL

                    SELECT well_name, wml_file_path || '\' || wml_file_name AS pathfile,
                           wml_load_by AS loadBy, TO_CHAR(wml_load_date,'dd-MON-yyyy') loadDate,
                           NULL AS FOLDER, NULL AS FROM_TAB, NULL AS TAB_ROWID
                    FROM well_master_log
                    WHERE {whereClause}
                )
                SELECT * FROM one ORDER BY 1, 2";
            }
        }
        public static string BuildWELLFILE(string whereClause, string type,string x)
        {
            if (type == "WF LIKE")
            {
                return $@"
            SELECT 
                well_name,
                wf_file_path || '\' || wf_file_name AS pathfile,
                wf_load_by AS loadBy,
                TO_CHAR(wf_loaded_date, 'dd-MON-yyyy') AS loadDate,
                NULL AS FOLDER,
                NULL AS FROM_TAB,
                NULL AS TAB_ROWID
            FROM well_file
            WHERE {whereClause}
            AND WF_SUBJECT LIKE '{x}'
            ORDER BY 1, 2";
            }
            else if (type == "WF IN") // <-- Ganti sesuai kebutuhan logika
            {
                return $@"
            SELECT 
                well_name,
                wf_file_path || '\' || wf_file_name AS pathfile,
                wf_load_by AS loadBy,
                TO_CHAR(wf_loaded_date, 'dd-MON-yyyy') AS loadDate,
                NULL AS FOLDER,
                NULL AS FROM_TAB,
                NULL AS TAB_ROWID
            FROM well_file
            WHERE {whereClause}
            AND WF_SUBJECT in {x}
            ORDER BY 1, 2";
            }

            // Default fallback (bisa dikembalikan error atau kosong)
            return $@"
            SELECT 
                well_name,
                wf_file_path || '\' || wf_file_name AS pathfile,
                wf_load_by AS loadBy,
                TO_CHAR(wf_loaded_date, 'dd-MON-yyyy') AS loadDate,
                NULL AS FOLDER,
                NULL AS FROM_TAB,
                NULL AS TAB_ROWID
            FROM well_file
            WHERE {whereClause}
            ORDER BY 1, 2";
        }
        public static string BuildWhereClause(string type, string value, string queryMode)
        {
            switch (type)
            {
                case "Well Name Like":
                    return $"well_name LIKE '{value}'";
                case "Struc Name =":
                    //return $"lookup.get_structure_name(well_name) = '{value}'";
                    if (queryMode == "WHP Otomatis")
                    {
                        return $"lookup.get_structure_name(well_name) = '{value}'";
                    }
                    else
                    {
                        return $@"
                    well_name IN (
                        SELECT well_name 
                        FROM well 
                        WHERE structure_s IN (
                            SELECT structure_s 
                            FROM structure 
                            WHERE nama_struktur = '{value}'
                            UNION
                            SELECT structure_s 
                            FROM structure 
                            WHERE s_parent = (
                                SELECT structure_s 
                                FROM structure 
                                WHERE nama_struktur = '{value}'
                            )
                        )
                    )";
                    }
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
                    if (queryMode == "WHP Otomatis")
                    {
                        return $"(WHP_DOC_FILE_NAME LIKE '{value}' OR WHP_WS_FILE_NAME LIKE '{value}')";
                    }
                    else if (queryMode =="WELL FILE")
                    {
                        return $@"
                            upper(wf_file_name) like '%SPA-013%'
                            ";
                    }
                    else
                    {
                        return value.ToUpper();
                    }                                       
                case "Well Name In":
                    return $"well_name IN {value}";
                case "Struc Name In":
                    if (queryMode == "WHP Otomatis")
                    {
                        return $"lookup.get_structure_name(well_name) IN {value}";
                    }
                    else if(queryMode == "WELL LOG" || queryMode == "WELL FILE")
                    {
                        return $@"
                            well_name IN (
                              SELECT well_name
                              FROM well
                              WHERE structure_s IN (
                                  SELECT structure_s
                                  FROM structure
                                  WHERE nama_struktur IN {value}
                                  UNION
                                  SELECT structure_s
                                  FROM structure
                                  WHERE s_parent IN (
                                      SELECT structure_s
                                      FROM structure
                                      WHERE nama_struktur IN {value}
                                  )
                              )
                            )
                            ";
                    }
                    else
                    {
                        return $@"
                        
                    ";
                    }
                //
                default:
                    return "1=1";
            }
        }
    }
}
