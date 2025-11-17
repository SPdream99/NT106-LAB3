using System;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

public static class DatabaseHelper
{
    private const string DatabaseFileName = "Database.db";
    public class MonAn
    {
        public string TenMon { get; set; }
        public string HinhAnh { get; set; }
        public string NguoiDung { get; set; }
    }
    public static void InitializeDatabase()
    {
        if (!File.Exists(DatabaseFileName))
        {
            SQLiteConnection.CreateFile(DatabaseFileName);

            using (var conn = new SQLiteConnection($"Data Source={DatabaseFileName};Version=3;"))
            {
                conn.Open();

                string sqlNguoiDung = @"
                CREATE TABLE IF NOT EXISTS NguoiDung (
                    IDNCC INTEGER PRIMARY KEY AUTOINCREMENT,
                    HoVaTen TEXT NOT NULL,
                    QuyenHan TEXT NOT NULL
                );";

                string sqlMonAn = @"
                CREATE TABLE IF NOT EXISTS MonAn (
                    IDMA INTEGER PRIMARY KEY AUTOINCREMENT,
                    TenMonAn TEXT NOT NULL,
                    HinhAnh TEXT,
                    IDNCC INTEGER,
                    FOREIGN KEY(IDNCC) REFERENCES NguoiDung(IDNCC)
                );";

                using (var cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = sqlNguoiDung;
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = sqlMonAn;
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
    public static List<string> FoodList()
    {
        List<string> list = new List<string>();

        using (var conn = new SQLiteConnection($"Data Source={DatabaseFileName};Version=3;"))
        {
            conn.Open();
            string sql = "SELECT TenMonAn FROM MonAn";

            using (var cmd = new SQLiteCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(reader["TenMonAn"].ToString());
                }
            }
        }

        return list;
    }
    public static MonAn SelectFood()
    {
        MonAn selected = null;

        using (var conn = new SQLiteConnection($"Data Source={DatabaseFileName};Version=3;"))
        {
            conn.Open();
            string sql = @"SELECT MonAn.TenMonAn, MonAn.HinhAnh, NguoiDung.HoVaTen 
                       FROM MonAn 
                       LEFT JOIN NguoiDung ON MonAn.IDNCC = NguoiDung.IDNCC";

            using (var cmd = new SQLiteCommand(sql, conn))
            using (var reader = cmd.ExecuteReader())
            {
                var list = new List<MonAn>();
                while (reader.Read())
                {
                    list.Add(new MonAn
                    {
                        TenMon = reader["TenMonAn"].ToString(),
                        HinhAnh = reader["HinhAnh"].ToString(),
                        NguoiDung = reader["HoVaTen"].ToString()
                    });
                }
                if (list.Count > 0)
                {
                    Random rnd = new Random();
                    selected = list[rnd.Next(list.Count)];
                }
            }
        }
        return selected;
    }
    public static bool CheckUser(string hoVaTen)
    {
        using (var conn = new SQLiteConnection($"Data Source={DatabaseFileName};Version=3;"))
        {
            conn.Open();
            string sql = "SELECT COUNT(*) FROM NguoiDung WHERE HoVaTen = @ten";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ten", hoVaTen);
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }
    }
    public static int AddUser(string hoVaTen)
    {
        using (var conn = new SQLiteConnection($"Data Source={DatabaseFileName};Version=3;"))
        {
            conn.Open();
            string sql = "INSERT INTO NguoiDung(HoVaTen, QuyenHan) VALUES(@ten, 'User')";
            using (var cmd = new SQLiteCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@ten", hoVaTen);
                cmd.ExecuteNonQuery();
                return (int)conn.LastInsertRowId;
            }
        }
    }
    public static void AddFood(string tenMon, string hinhAnh, string hoVaTenNguoiDung)
    {
        using (var conn = new SQLiteConnection($"Data Source={DatabaseFileName};Version=3;"))
        {
            conn.Open();
            int idNguoiDung;
            if (CheckUser(hoVaTenNguoiDung))
            {
                string sqlGetID = "SELECT IDNCC FROM NguoiDung WHERE HoVaTen=@ten";
                using (var cmd = new SQLiteCommand(sqlGetID, conn))
                {
                    cmd.Parameters.AddWithValue("@ten", hoVaTenNguoiDung);
                    idNguoiDung = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            else
            {
                idNguoiDung = AddUser(hoVaTenNguoiDung);
            }
            string sqlInsertMonAn = "INSERT INTO MonAn(TenMonAn,HinhAnh,IDNCC) VALUES(@tenMon,@hinhAnh,@idNguoiDung)";
            using (var cmd = new SQLiteCommand(sqlInsertMonAn, conn))
            {
                cmd.Parameters.AddWithValue("@tenMon", tenMon);
                cmd.Parameters.AddWithValue("@hinhAnh", hinhAnh);
                cmd.Parameters.AddWithValue("@idNguoiDung", idNguoiDung);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
