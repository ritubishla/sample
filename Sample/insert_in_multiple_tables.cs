using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sample
{
    class A
    {
        private void FetchData()
        {
            string connetionString = null;
            SqlConnection cn;
            SqlCommand cmd;
            SqlDataAdapter ad = new SqlDataAdapter();
            DataSet ds = new DataSet();
            int i = 0;
            string firstSql = null;
            string secondSql = null;

            connetionString = "Data Source=DESKTOP-A2NTCHA\\SQLJITU;Initial Catalog=sample;integrated security=true";
            firstSql = "Your First SQL Statement Here";
            secondSql = "Your Second SQL Statement Here";
            cn = new SqlConnection(connetionString);

            try
            {
                cn.Open();
                firstSql = "insert into tblDepartment values(@DeptID,@DeptName,@No_of_Employee,@Location)";
                cmd.Parameters.AddWithValue("@DeptID", DeptID);
                cmd.Parameters.AddWithValue("@DeptName", DeptName);
                cmd.Parameters.AddWithValue("@No_of_Employee", No_of_Employee);
                cmd.Parameters.AddWithValue("@Location", Location);
                secondSql = "insert into tblManager values(@FirstName,@Exp,@Deptid,@Phone,@Address,@emailId)";


                cmd = new SqlCommand(firstSql,cn);
                ad.SelectCommand = cmd;
                ad.Fill(ds, "First Table");

                ad.SelectCommand.CommandText = secondSql;
                ad.Fill(ds, "Second Table");

                ad.Dispose();
                cmd.Dispose();
                cn.Close();

                //retrieve first table data 
                for (i = 0; i <= ds.Tables[0].Rows.Count - 1; i++)
                {
                    Console.WriteLine(ds.Tables[0].Rows[i].ItemArray[0] + " -- " + ds.Tables[0].Rows[i].ItemArray[1]);
                }
                //retrieve second table data 
                for (i = 0; i <= ds.Tables[1].Rows.Count - 1; i++)
                {
                    Console.WriteLine(ds.Tables[1].Rows[i].ItemArray[0] + " -- " + ds.Tables[1].Rows[i].ItemArray[1]);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Can not open connection ! ");
            }
        }

    }
}
