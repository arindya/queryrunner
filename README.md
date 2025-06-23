# QueryRunner

**QueryRunner** adalah aplikasi desktop berbasis **C# WinForms** yang memungkinkan pengguna menjalankan query SQL terhadap database Oracle dengan mudah, cepat, dan terstruktur. Aplikasi ini mendukung input koneksi dinamis, penyimpanan kredensial secara lokal, serta ekspor hasil ke Excel.

---

## 🚀 Fitur Utama

- 🔌 **Koneksi Dinamis ke Oracle**
  - User ID, Password, Host, Port, dan Service Name dapat diatur dan disimpan ke local user settings.
  
- 📝 **Editor Query Interaktif**
  - TextBox untuk memasukkan SQL dengan dukungan multi-line.
  
- 📊 **Hasil Query dalam Tabel**
  - Menampilkan hasil eksekusi query dalam `DataGridView`.

- 📤 **Ekspor ke Excel**
  - Dukungan ekspor hasil ke file `.xlsx` menggunakan ClosedXML atau EPPlus.

- 💾 **Penyimpanan Konfigurasi**
  - Detail koneksi disimpan dalam `Properties.Settings.Default` (user scope).

---

## ⚙️ How To RUN?

### 1. **Persiapan**
- Install Visual Studio 2022 atau lebih baru.
- Pastikan sudah menginstall NuGet package berikut:
  - `Oracle.ManagedDataAccess`
  - `ClosedXML` atau `EPPlus` (pilih salah satu untuk ekspor Excel)

### 2. **Konfigurasi Koneksi**
- Jalankan aplikasi.
- Masukkan:
  - Oracle User ID
  - Oracle Password
  - Host (contoh: `10.111.1.111`)
  - Port (default: `1521`)
  - Service Name (contoh: `xx`)
- Klik tombol **"Simpan Koneksi"**

### 3. **Menjalan**
