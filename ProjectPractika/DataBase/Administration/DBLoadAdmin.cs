using ProjectPractika.Models;
using ProjectPractika.Models.Helper_models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectPractika.DataBase.Administration
{
    public class DBLoadAdmin : Connect
    {

        public ObservableCollection<Specialization> GetAllSpecializations()
        {
            ObservableCollection<Specialization> specializations = new ObservableCollection<Specialization>();
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
                        specializations.Add(new Specialization(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2)));
                }
                reader.Close();
                base.Close();
                return specializations;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка специальностей");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<Entrant> GetAllEntrantsPagination(int offset, int limit)
        {
            ObservableCollection<Entrant> entrants = new ObservableCollection<Entrant>();
            Open();
            try {
                if (status)
                {
                    string sqlExpression = "AdminApp.AllEntrantsPagination";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter offsetParam = new SqlParameter
                    {
                        ParameterName = "@offset",
                        Value = offset
                    };
                    SqlParameter limitParam = new SqlParameter
                    {
                        ParameterName = "@limit",
                        Value = limit
                    };
                    // добавляем параметры
                    command.Parameters.Add(offsetParam);
                    command.Parameters.Add(limitParam);

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            entrants.Add(new Entrant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                reader.GetInt32(3), reader.GetInt32(4)));
                    }
                    reader.Close();
                    base.Close();
                    return entrants;
                }
                else
                {
                    MessageBox.Show("Error: ошибка получения списка абитуриентов");
                    base.Close();
                    return null;
                }

            }
            catch (Exception e) {
                MessageBox.Show("Error: ошибка получения списка абитуриентов" + "\n\n" + e.Message);
            }
            return null;
           
        }

        public ObservableCollection<Entrant> GetAllEntrantsByName(string fullName)
        {
            ObservableCollection<Entrant> entrants = new ObservableCollection<Entrant>();
            Open();
            if (status)
            {
                string sqlExpression = "AdminApp.SearchEntrantByFullName";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@fullName",
                    Value = fullName
                };
                // добавляем параметры
                command.Parameters.Add(nameParam);
              
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        entrants.Add(new Entrant(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetInt32(3), reader.GetInt32(4)));
                }
                reader.Close();
                base.Close();
                return entrants;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка абитуриентов");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<EntryWithInfo> GetAllEntryWithInfo(int year, int isFree, int isIntramural, string fullName)
        {
            ObservableCollection<EntryWithInfo> entries = new ObservableCollection<EntryWithInfo>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchEntryByInfo";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
                SqlParameter fullNameParam = new SqlParameter
                {
                    ParameterName = "@fullName",
                    Value = fullName
                };
                SqlParameter yearParam = new SqlParameter
                {
                    ParameterName = "@dateYear",
                    Value = year
                };
                SqlParameter isFreeParam = new SqlParameter
                {
                    ParameterName = "@isFree",
                    Value = isFree
                };
                SqlParameter isIntramuralParam = new SqlParameter
                {
                    ParameterName = "@isIntramural",
                    Value = isIntramural
                };
               
                // добавляем параметры
                command.Parameters.Add(yearParam);
                command.Parameters.Add(isFreeParam);
                command.Parameters.Add(isIntramuralParam);
                command.Parameters.Add(fullNameParam);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        entries.Add(new EntryWithInfo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
                    
                }
                reader.Close();
                base.Close();
                return entries;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка записей");
                base.Close();
                return null;
            }
        }


        #region DeleteInfo

        public void DeleteCategory(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteCategory";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                   
                    // параметры для ввода 
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = id
                    };
                 
                    // добавляем параметры
                    command.Parameters.Add(loginParam);

                    command.ExecuteNonQuery();

                    base.Close();
                }
            }
            catch (Exception e) {
                System.Windows.MessageBox.Show("Error: ошибка удаления категории" + "\n\n" + e.Message);
                base.Close();
            }  
        }

        public void DeleteEducationalIns(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteEduIns";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@eduId",
                        Value = id
                    };

                    // добавляем параметры
                    command.Parameters.Add(loginParam);

                    command.ExecuteNonQuery();

                    base.Close();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка удаления учебного заведения" + "\n\n" + e.Message);
                base.Close();
            }

        }

        public void DeleteEntrant(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteEntrant";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@entrantId",
                        Value = id
                    };

                    // добавляем параметры
                    command.Parameters.Add(loginParam);

                    command.ExecuteNonQuery();

                    base.Close();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка удаления абитуриента" + "\n\n" + e.Message);
                base.Close();
            }

        }

        public void DeleteConcourse(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteConcourse";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@conId",
                        Value = id
                    };

                    // добавляем параметры
                    command.Parameters.Add(loginParam);

                    command.ExecuteNonQuery();

                    base.Close();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка удаления конкурса" + "\n\n" + e.Message);
                base.Close();
            }

        }

        public void DeleteEntry(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteEntry";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@entryId",
                        Value = id
                    };

                    // добавляем параметры
                    command.Parameters.Add(loginParam);

                    command.ExecuteNonQuery();

                    base.Close();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка удаления записи" + "\n\n" + e.Message);
                base.Close();
            }
        }

        public void DeleteSpecialization(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteSpecialization";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter loginParam = new SqlParameter
                    {
                        ParameterName = "@specializationId",
                        Value = id
                    };

                    // добавляем параметры
                    command.Parameters.Add(loginParam);

                    command.ExecuteNonQuery();

                    base.Close();
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка удаления специальности в учреждении" + "\n\n" + e.Message);
                base.Close();
            }
        }
        #endregion
    }
}
