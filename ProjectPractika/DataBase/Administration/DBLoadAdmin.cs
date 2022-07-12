/*using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPractika.DataBase.Administration
{
    public class DBLoadAdmin : Connect
    {
        public ObservableCollection<Models.Specialization> GetAllEntrants()
        {
            ObservableCollection<Models.Specialization> specializations = new ObservableCollection<Models.Specialization>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllSpecialization";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        specializations.Add(new Models.Specialization(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                }
                reader.Close();
                base.Close();
                return specializations;
            }
            else
            {
                System.Windows.MessageBox.Show("Error: ошибка получения списка специальностей");
                base.Close();
                return null;
            }
        }
    }
}
*/