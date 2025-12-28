using Microsoft.Data.SqlClient;

namespace MSK_PC_Controller.Services;

public sealed class AuthService
{
    public bool CheckLogin(string user, string pass)
    {
        using var con = Db.Conn();
        using var cmd = new SqlCommand(
            "SELECT COUNT(1) FROM dbo.Users WHERE Name=@u AND Password=@p", con);

        cmd.Parameters.AddWithValue("@u", user);
        cmd.Parameters.AddWithValue("@p", pass);

        con.Open();
        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
    }
}
