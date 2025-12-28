using System.Data;
using Microsoft.Data.SqlClient;

namespace MSK_PC_Controller.Services;

internal sealed class PcService
{
    public void InsertUserPc(string userName, string ip, string name)
    {
        using var con = Db.Conn();
        using var cmd = new SqlCommand(
            "INSERT INTO PC (UserName, PcName, IpAdress) VALUES (@u, @n, @i)", con);

        cmd.Parameters.AddWithValue("@u", userName);
        cmd.Parameters.AddWithValue("@n", name);
        cmd.Parameters.AddWithValue("@i", ip);

        con.Open();
        cmd.ExecuteNonQuery();
    }

    public DataTable GetUserPcs(string userName)
    {
        using var con = Db.Conn();
        using var cmd = new SqlCommand("SELECT PcName, IpAdress FROM PC WHERE UserName=@u", con);
        using var da = new SqlDataAdapter(cmd);

        cmd.Parameters.AddWithValue("@u", userName);
        var dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
}
