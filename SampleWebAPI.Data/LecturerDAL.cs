
using SimpleWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;


namespace SampleWebAPI.Data
{
    public class LecturerDAL
    {
        private string GetConnString()
        {
            return "Data Source=PRUSHKA\\MSSQLSERVER01;Initial Catalog=SampleAdoDb;Integrated Security=SSPI";
        }
        //GetAll menggunakan Ado.net biasa
        /*public IEnumerable<Lecturer> GetAll()
        {
            List<Lecturer> lstLecturers = new List<Lecturer>();
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from lecturers order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstLecturers.Add(new Lecturer()
                        {
                            Nik = dr["Nik"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            Alamat = dr["Alamat"].ToString(),
                            Telp = dr["Telp"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstLecturers;

        }*/
        //GetAll menggunakan Dapper
        public IEnumerable<Lecturer> GetAll()
        {

            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from lecturers order by Nama asc";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                var result = conn.Query<Lecturer>(strSql);
                return result;
            }
        }
        //GetByid menggunakan ADO.net biasa
        /*
                    public Lecturer GetById(string nik)
                {
                    Lecturer lecturer = new Lecturer();
                    using (SqlConnection conn = new SqlConnection(GetConnString()))
                    {
                        string strSql = @"select * from lecturers where Nik = @Nik";
                        SqlCommand cmd = new SqlCommand(strSql, conn);
                        cmd.Parameters.AddWithValue("@Nik", nik);
                        conn.Open();
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dr.Read();
                            lecturer.Nik = dr["Nik"].ToString();
                            lecturer.Nama = dr["Nama"].ToString();
                            lecturer.Alamat = dr["Alamat"].ToString();
                            lecturer.Telp = dr["Telp"].ToString();
                        }
                        dr.Close();
                        cmd.Dispose();
                        conn.Close();



                    }
                    return lecturer;

                }*/
        //GetByID menggunakan Dapper
        public Lecturer GetById(string nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from lecturers where Nik = @Nik";
                var param = new { Nik = nik };
                var result = conn.QueryFirst<Lecturer>(strSql, param);
                return result;



            }

        }
        //insert menggunakan ADO.net biasa
        /*
        public void Insert(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"insert into Lecturers(Nik,Nama,Alamat,Telp) Values (@Nik,@Nama,@Alamat,@Telp)";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nik", lecturer.Nik);
                cmd.Parameters.AddWithValue("@Nama", lecturer.Nama);
                cmd.Parameters.AddWithValue("@Alamat", lecturer.Alamat);
                cmd.Parameters.AddWithValue("@Telp", lecturer.Telp);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception("Gagal Menambah Data");
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Number == 2627)
                        throw new Exception($"Data {lecturer.Nik} Sudah Ada, inputkan nik yang lain..");
                    else
                        throw new Exception($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }


        }
        */
        //insert menggunakan Dapper
        public void Insert(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"insert into Lecturers(Nik,Nama,Alamat,Telp) Values (@Nik,@Nama,@Alamat,@Telp)";

               var param = new {Nik=lecturer.Nik,Nama=lecturer.Nama,Alamat=lecturer.Alamat,Telp=lecturer.Telp};

                try
                {
                    int result = conn.Execute(strSql, param);
                    if (result != 1)
                        throw new Exception("Gagal Menambah Data");
                }
                catch (SqlException sqlEx)
                {
                    
                        throw new Exception($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
                
            }


        }
        //update menggunakan ADO.net biasa
        /*public void Update(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"update Lecturers set Nama=@Nama,Alamat=@Alamat,Telp=@Telp where Nik=@Nik ";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", lecturer.Nama);
                cmd.Parameters.AddWithValue("@Alamat", lecturer.Alamat);
                cmd.Parameters.AddWithValue("@Telp", lecturer.Telp);
                cmd.Parameters.AddWithValue("@Nik", lecturer.Nik);

                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                        throw new Exception($"Update data {lecturer.Nama} Gagal");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }
            }

        }*/
        //update menggunakan Dapper
        public void Update(Lecturer lecturer)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"update Lecturers set Nama=@Nama,Alamat=@Alamat,Telp=@Telp where Nik=@Nik ";

                var param = new { Nama = lecturer.Nama, Alamat = lecturer.Alamat, Telp = lecturer.Telp, Nik = lecturer.Nik };

                try
                {
                    int result = conn.Execute(strSql, param);
                    if (result != 1)
                        throw new Exception($"Update data {lecturer.Nama} Gagal");
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.InnerException.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
               
            }

        }
        //delete menggunakan Ado.net Biasa
        /*
        public void Delete(string Nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"delete from Lecturers where Nik=@Nik";

                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nik",Nik);
                try
                {
                    conn.Open();
                    var result = cmd.ExecuteNonQuery();
                    if (result != 1)
                    {
                        throw new Exception($"Gagal Delete Data");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
                finally
                {
                    cmd.Dispose();
                    conn.Close();
                }


            }

        }*/
        // delete menggunakan Dapper
        public void Delete(string Nik)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"delete from Lecturers where Nik=@Nik";

                var param = new { Nik = Nik };
                try
                {
                    var result = conn.Execute(strSql, param);
                    if (result != 1)
                    {
                        throw new Exception($"Gagal Delete Data");
                    }
                }
                catch (SqlException sqlEx)
                {
                    throw new Exception($"{sqlEx.Message} - {sqlEx.Number}");
                }
                catch (Exception ex)
                {
                    throw new Exception($" {ex.Message}");
                }
                


            }

        }
        //GetByNama menggunakan Ado.net biasa
        /*public IEnumerable<Lecturer> GetByNama(string Nama)
        {
            List<Lecturer> lstLecturers = new List<Lecturer>();
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from lecturers where Nama like @Nama";
                SqlCommand cmd = new SqlCommand(strSql, conn);
                cmd.Parameters.AddWithValue("@Nama", $"%{Nama}%");
                conn.Open();
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        lstLecturers.Add(new Lecturer()
                        {
                            Nik = dr["Nik"].ToString(),
                            Nama = dr["Nama"].ToString(),
                            Alamat = dr["Alamat"].ToString(),
                            Telp = dr["Telp"].ToString()
                        });
                    }
                }
                dr.Close();
                cmd.Dispose();
                conn.Close();
            }
            return lstLecturers;

        }*/

        //GetByNama Menggunakan Dapper
        public IEnumerable<Lecturer> GetByNama(string Nama)
        {
            using (SqlConnection conn = new SqlConnection(GetConnString()))
            {
                string strSql = @"select * from lecturers where Nama like @Nama";
                var param = new { Nama = $"%{Nama}%" };
                var result = conn.Query<Lecturer>(strSql, param);
                return result;

            }
        }

    }
}
