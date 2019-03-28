using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Sample
{
    class Program
    {
        //add a comment here to show changes in the github
        //hii there is some changes
        //static SqlConnection cn;
        //hii hii
        static void Main(string[] args)
        {
            //searchDept();
            // insertDept();
            //updateDept();
            // InsertManager();
            //storedprocedure();
            //OpertionsDataset();
            SearchEmpWithMsg();
        }
        static void insertDept()
        {
            SqlConnection cn = new SqlConnection(@"data source=DESKTOP-A2NTCHA\SQLJITU;initial catalog=sample;integrated security=true;");
            //string query = "insert into tblDepartment values('1','HR',10,'Delhi')";
            //string query = "update tblDepartment set Location='Gurugram'";
            //string query = "Delete from tblDepartment where DeptName='Sales'";
            //string query = "insert into tblManager values('Daniel',5,(select DeptID from tblDepartment where DeptName='HR'),8708590602,'delhi','daniel@outlook.com')";
            //string query = "update tblManager set Phone=8575452147";
            // string query = "Delete from tblManager where Address='Delhi'";
            Console.WriteLine("Enter depid");
            int DeptID = Convert.ToByte(Console.ReadLine());
            Console.WriteLine("Enter depname");
            string DeptName = Console.ReadLine();
            Console.WriteLine("Enter number of employee");
            int No_of_Employee = Convert.ToByte(Console.ReadLine());
            Console.WriteLine("Enter Location");
            string Location = Console.ReadLine();
            // string query = "insert into tblDepartment values(@DeptID,@DeptName,@No_of_Employee,@Location)";
            string query = "update tblDepartment set Location=@Location where DeptName=@DeptName";
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.Parameters.AddWithValue("@DeptID", DeptID);
            cmd.Parameters.AddWithValue("@DeptName", DeptName);
            cmd.Parameters.AddWithValue("@No_of_Employee", No_of_Employee);
            cmd.Parameters.AddWithValue("@Location", Location);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
            Console.WriteLine("table created");

        }
        static void updateDept()
        {
            using(SqlConnection cn = new SqlConnection(@"data source=DESKTOP-A2NTCHA\SQLJITU;initial catalog=sample;integrated security=true;"))
                {
                string query = "update tblDepartment set Location=@Location where DeptName=@DeptName";
                SqlCommand cmd = new SqlCommand(query, cn);
                Console.WriteLine("Enter Location");
                cmd.Parameters.AddWithValue("@Location", Console.ReadLine());
                Console.WriteLine("Enter DepartmentName");
                cmd.Parameters.AddWithValue("@DeptName", Console.ReadLine());
                cn.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Record updated");
            }
        }
        static void searchDept()
        {
            using (SqlConnection cn = new SqlConnection(@"data source=DESKTOP-A2NTCHA\SQLJITU;initial catalog=sample;integrated security=true;"))
            {
                string query = "Select * from tblDepartment where DeptName=@DeptName";
                SqlCommand cmd = new SqlCommand(query, cn);
                Console.WriteLine("Enter department name");
                cmd.Parameters.AddWithValue("@DeptName", Console.ReadLine());
                cn.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    while(dr.Read())
                    {
                        for(int i=0;i<dr.FieldCount;i++)
                        {
                            Console.Write(dr[i]+ " ");
                           
                        }
                    }
                    int id =(int) dr[0];
                }
                Console.WriteLine("your record is here");
            }
        }
        static void InsertManager()
        {
            using (SqlConnection cn = new SqlConnection(@"data source=DESKTOP-A2NTCHA\SQLJITU;initial catalog=sample;integrated security=true;"))
            {
                string query = "insert into tblManager values(@FirstName,@Exp,@Deptid,@Phone,@Address,@emailId)";
                
                SqlCommand cmd1 = new SqlCommand("Select DeptID from tblDepartment", cn);
                cn.Open();
               // cmd1.Parameters.AddWithValue("@Deptid", Convert.ToByte(Console.ReadLine()));
                SqlDataReader dr = cmd1.ExecuteReader();
                int id = dr.GetOrdinal("Deptid");
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            Console.Write(dr[i] + " ");
                            //// where supposingly ID is the field name But this FK_ID will be the one you want only if the query returns a single result, otherwise it will set FK_ID 
                            //  //to the last returned row
                            //id = (int)dr[i];
                            Console.WriteLine("{0}\t{1}", dr.GetInt32(0));
                        }

                    }
                }
                
                dr.Close();
                cmd1.Dispose();
                cn.Close();
                SqlCommand cmd = new SqlCommand(query, cn);

                Console.WriteLine("Enter FirstName");
                cmd.Parameters.AddWithValue("@FirstName", Console.ReadLine());
                Console.WriteLine("Enter exp");
                cmd.Parameters.AddWithValue("@Exp", Convert.ToByte(Console.ReadLine()));
                Console.WriteLine("Enter DeptID");
                //cmd.Parameters.AddWithValue("@Deptid", Convert.ToByte(Console.ReadLine()));
                cmd.Parameters.AddWithValue("@Deptid", id);
                Console.WriteLine("Enter Phone");
                cmd.Parameters.AddWithValue("@Phone",Console.ReadLine());
                Console.WriteLine("Enter Address");
                cmd.Parameters.AddWithValue("@Address", Console.ReadLine());
                Console.WriteLine("Enter emailId");
                cmd.Parameters.AddWithValue("@emailId", Console.ReadLine());
                cn.Open();
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
        }
       public static void storedprocedure()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con1"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("student_insert",cn))
                {
                    SqlParameter p = new SqlParameter("@flag", SqlDbType.Int);
                    p.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(p);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Console.WriteLine("Enter id ");
                    cmd.Parameters.AddWithValue("@id", Convert.ToInt16(Console.ReadLine()));
                    Console.WriteLine("Enter name");
                    cmd.Parameters.AddWithValue("@name", Console.ReadLine());
                    Console.WriteLine("Enter contact");
                    cmd.Parameters.AddWithValue("@contact", Console.ReadLine());
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    int flag = (int)cmd.Parameters["@flag"].Value;
                    if(flag==0)
                    {
                        Console.WriteLine("Id already exists");
                    }
                    else
                    {
                        Console.WriteLine("Record Inserted");
                    }
                }
            }

        }
        static void OpertionsDataset()
        {
            using (SqlConnection cn = new SqlConnection(@"data source=DESKTOP-A2NTCHA\SQLJITU;initial catalog=sample;integrated security=true;"))
            {
                
                string query = "Select * from tblDepartment";
                SqlDataAdapter ad = new SqlDataAdapter(query,cn);
                SqlCommandBuilder sb = new SqlCommandBuilder(ad);
                DataSet ds = new DataSet();
                ad.Fill(ds);
                foreach(DataRow dr in ds.Tables[0].Rows)
                {
                    foreach(DataColumn dc in ds.Tables[0].Columns)
                    {
                        Console.WriteLine(dr[dc] + " ");
                    }
                }
                DataRow dr1 = ds.Tables[0].NewRow();
                dr1[0] = 6;
                    dr1[1] = "cse";
                dr1[2] = 30;
                dr1[3] = "Mumbai";
                ds.Tables[0].Rows.Add(dr1);
                ad.Update(ds);

            }
        }
        public static void SearchEmpWithMsg()
        {
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["con1"].ToString()))
            {
                using (SqlCommand cmd = new SqlCommand("SearchEmpWithMsg", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    Console.WriteLine("Enter id to search the record");
                    cmd.Parameters.AddWithValue("@id", Convert.ToByte(Console.ReadLine()));
                    SqlParameter p = new SqlParameter("@flag", SqlDbType.Int);
                   // SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
                    SqlParameter p2 = new SqlParameter("@name", SqlDbType.VarChar, 20);
                    SqlParameter p3 = new SqlParameter("@address", SqlDbType.VarChar, 20);
                    SqlParameter p4 = new SqlParameter("@dept", SqlDbType.VarChar, 20);
                    SqlParameter p5 = new SqlParameter("@exp", SqlDbType.Int);
                    p.Direction = ParameterDirection.ReturnValue;
                    p2.Direction = ParameterDirection.Output;
                    p3.Direction = ParameterDirection.Output;
                    p4.Direction = ParameterDirection.Output;
                    p5.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cn.Open();
                    cmd.ExecuteNonQuery();
                   
                    int flag = (int)cmd.Parameters["@flag"].Value;
                    if(flag==1)
                    {
                        Console.WriteLine("Name is "+cmd.Parameters["@name"].Value.ToString());
                        Console.WriteLine("dept is " + cmd.Parameters["@dept"].Value.ToString());
                        Console.WriteLine("exp is " + cmd.Parameters["@exp"].Value.ToString());
                        Console.WriteLine("address is " + cmd.Parameters["@address"].Value.ToString());
                    }
                    else
                    {
                        Console.WriteLine("Id does not exists");
                    }
                }
                }

                }                
                }
}

