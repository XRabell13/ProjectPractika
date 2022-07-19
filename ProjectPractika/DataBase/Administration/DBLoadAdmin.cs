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

        public ObservableCollection<SpecializationEducation> GetAllSpecializationEducationByInfo(string specName)
        {
            ObservableCollection<SpecializationEducation> specializationEducations = new ObservableCollection<SpecializationEducation>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchSpecialization";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                SqlParameter specNameParam = new SqlParameter
                {
                    ParameterName = "@specName",
                    Value = specName
                };
                // добавляем параметры
                command.Parameters.Add(specNameParam);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        specializationEducations.Add(new SpecializationEducation(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
                base.Close();
                return specializationEducations;
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

        public ObservableCollection<EntryDG> GetAllEntriesDG()
        {
            ObservableCollection<EntryDG> entries = new ObservableCollection<EntryDG>();
            Open();
            if (status)
            {
                string sqlExpression = "AdminApp.AllEntriesWithConcourseAndEntrant";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        entries.Add(new EntryDG(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3),
                            reader.GetBoolean(4), reader.GetBoolean(5), reader.GetInt32(6)));

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

        public void DeleteSpecializationByEdu(int id)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.DeleteSpecializationByEdu";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@specEduId",
                        Value = id
                    };

                    // добавляем параметры
                    command.Parameters.Add(idParam);

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

        #region AddInfo

        public bool AddCategory(string categoryName)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddCategory";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter categoryParam = new SqlParameter
                    {
                        ParameterName = "@categoryName",
                        Value = categoryName
                    };

                    // добавляем параметры
                    command.Parameters.Add(categoryParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления категории" + "\n\n" + e.Message);
                base.Close();
                return false;
            }

            return false;
        }

        public bool AddSpecialization(string specName, int categoryId)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddSpecialization";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter specNameParam = new SqlParameter
                    {
                        ParameterName = "@specName",
                        Value = specName
                    };
                    SqlParameter categoryIdParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = categoryId
                    };

                    // добавляем параметры
                    command.Parameters.Add(specNameParam);
                    command.Parameters.Add(categoryIdParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления специальности" + "\n\n" + e.Message);
                base.Close();
                return false;
            }

            return false;
        }

        public bool AddEduIns(string insName, string insAddress)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddEduIns";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter insNameParam = new SqlParameter
                    {
                        ParameterName = "@insName",
                        Value = insName
                    };
                    SqlParameter insAddressParam = new SqlParameter
                    {
                        ParameterName = "@adress",
                        Value = insAddress
                    };

                    // добавляем параметры
                    command.Parameters.Add(insNameParam);
                    command.Parameters.Add(insAddressParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();
                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления учебного учреждения" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool AddEntrant(string fullName, string passport, int maxBall, int dateYear)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddEntrant";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter fullNameParam = new SqlParameter
                    {
                        ParameterName = "@fullName",
                        Value = fullName
                    };
                    SqlParameter passportParam = new SqlParameter
                    {
                        ParameterName = "@numberPassport",
                        Value = passport
                    };
                    SqlParameter maxBallParam = new SqlParameter
                    {
                        ParameterName = "@maxBall",
                        Value = maxBall
                    };
                    SqlParameter dateYearParam = new SqlParameter
                    {
                        ParameterName = "@dateYear",
                        Value = dateYear
                    };

                    // добавляем параметры
                    command.Parameters.Add(fullNameParam);
                    command.Parameters.Add(passportParam);
                    command.Parameters.Add(maxBallParam);
                    command.Parameters.Add(dateYearParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления абитуриента" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool AddConcourse(int countSeats, int isFree, int isIntramural, int dateYear, int idSpecializationEducational)
        {
        
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddConcourse";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter countSeatsParam = new SqlParameter
                    {
                        ParameterName = "@countSeats",
                        Value = countSeats
                    };
                    SqlParameter yearParam = new SqlParameter
                    {
                        ParameterName = "@dateYear",
                        Value = dateYear
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
                    SqlParameter idSpecializationEducationalParam = new SqlParameter
                    {
                        ParameterName = "@idSpecializationEducational",
                        Value = idSpecializationEducational
                    };
                    // добавляем параметры
                    command.Parameters.Add(countSeatsParam);
                    command.Parameters.Add(yearParam);
                    command.Parameters.Add(isFreeParam);
                    command.Parameters.Add(isIntramuralParam);
                    command.Parameters.Add(idSpecializationEducationalParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления конкурса" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool AddEntry(int idEntrant, int idConcourse)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddEntry";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idEntrantParam = new SqlParameter
                    {
                        ParameterName = "@idEntrant",
                        Value = idEntrant
                    };
                    SqlParameter idConcourseParam = new SqlParameter
                    {
                        ParameterName = "@idConcourse",
                        Value = idConcourse
                    };

                    // добавляем параметры
                    command.Parameters.Add(idEntrantParam);
                    command.Parameters.Add(idConcourseParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();
                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления записи" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool AddEduSpec(int idSpecialization, int idEducation)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.AddEducationalInsSpecializations";
                    bool added = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idEduParam = new SqlParameter
                    {
                        ParameterName = "@educationalInsID",
                        Value = idEducation
                    };
                    SqlParameter idSpecParam = new SqlParameter
                    {
                        ParameterName = "@specializationID",
                        Value = idSpecialization
                    };

                    // добавляем параметры
                    command.Parameters.Add(idSpecParam);
                    command.Parameters.Add(idEduParam);

                    added = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();
                    return added;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка добавления специальности в учебное учреждение" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }
        #endregion

        #region UpdateInfo

        public bool UpdateEntrant(int id,string fullName, string passport, int maxBall, int dateYear)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateEntrant";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@entrantId",
                        Value = id
                    };
                    SqlParameter fullNameParam = new SqlParameter
                    {
                        ParameterName = "@fullName",
                        Value = fullName
                    };
                    SqlParameter passportParam = new SqlParameter
                    {
                        ParameterName = "@numberPassport",
                        Value = passport
                    };
                    SqlParameter maxBallParam = new SqlParameter
                    {
                        ParameterName = "@maxBall",
                        Value = maxBall
                    };
                    SqlParameter dateYearParam = new SqlParameter
                    {
                        ParameterName = "@dateYear",
                        Value = dateYear
                    };

                    // добавляем параметры
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(fullNameParam);
                    command.Parameters.Add(passportParam);
                    command.Parameters.Add(maxBallParam);
                    command.Parameters.Add(dateYearParam);


                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());
                    
                    base.Close();

                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления абитуриента" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool UpdateCategory(int id, string categoryName)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateCategory";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = id
                    };
                    SqlParameter categoryParam = new SqlParameter
                    {
                        ParameterName = "@categoryName",
                        Value = categoryName
                    };

                    // добавляем параметры
                    command.Parameters.Add(categoryParam);
                    command.Parameters.Add(idParam);

                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления категории" + "\n\n" + e.Message);
                base.Close();
                return false;
            }

            return false;
        }

        public bool UpdateEduIns(int id, string insName, string insAddress)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateEduIns";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@eduId",
                        Value = id
                    };
                    SqlParameter insNameParam = new SqlParameter
                    {
                        ParameterName = "@insName",
                        Value = insName
                    };
                    SqlParameter insAddressParam = new SqlParameter
                    {
                        ParameterName = "@adress",
                        Value = insAddress
                    };

                    // добавляем параметры
                    command.Parameters.Add(insNameParam);
                    command.Parameters.Add(insAddressParam);
                    command.Parameters.Add(idParam);

                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();
                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления учебного учреждения" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool UpdateSpecializationName(int id, string specName)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateSpecializationName";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@specializationId",
                        Value = id
                    };
                    SqlParameter specNameParam = new SqlParameter
                    {
                        ParameterName = "@specName",
                        Value = specName
                    };
                  
                    // добавляем параметры
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(specNameParam);

                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления специальности" + "\n\n" + e.Message);
                base.Close();
                return false;
            }

            return false;
        }

        public bool UpdateSpecializationCategory(int id, int idCategory)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateSpecializationCategory";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@specializationId",
                        Value = id
                    };
                    SqlParameter idCategoryParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = idCategory
                    };

                    // добавляем параметры
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(idCategoryParam);

                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления специальности" + "\n\n" + e.Message);
                base.Close();
                return false;
            }

            return false;
        }
        
        public bool UpdateConcourse(int id, int countSeats, int isFree, int isIntramural, int dateYear)
        {

            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateConcourse";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@conId",
                        Value = id
                    };
                    SqlParameter countSeatsParam = new SqlParameter
                    {
                        ParameterName = "@countSeats",
                        Value = countSeats
                    };
                    SqlParameter yearParam = new SqlParameter
                    {
                        ParameterName = "@dateYear",
                        Value = dateYear
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
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(countSeatsParam);
                    command.Parameters.Add(yearParam);
                    command.Parameters.Add(isFreeParam);
                    command.Parameters.Add(isIntramuralParam);
                   
                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления конкурса" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }
      
        public bool UpdateConcourse(int id, int idSpecializationEducational)
        {

            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateConcourseSpecEdu";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@conId",
                        Value = id
                    };
                   
                    SqlParameter idSpecializationEducationalParam = new SqlParameter
                    {
                        ParameterName = "@idSpecializationEducational",
                        Value = idSpecializationEducational
                    };
                    // добавляем параметры
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(idSpecializationEducationalParam);

                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();

                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления конкурса" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool UpdateEntryConcourse(int id, int idConcourse)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateEntryConcourse";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@entryId",
                        Value = id
                    };
                   
                    SqlParameter idConcourseParam = new SqlParameter
                    {
                        ParameterName = "@idConcourse",
                        Value = idConcourse
                    };

                    // добавляем параметры
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(idConcourseParam);

                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();
                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления записи" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        public bool UpdateEntryEntrant(int id, int idEntrant)
        {
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "AdminApp.UpdateEntryEntrant";
                    bool updatted = false;
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter idParam = new SqlParameter
                    {
                        ParameterName = "@entryId",
                        Value = id
                    };
                    SqlParameter idEntrantParam = new SqlParameter
                    {
                        ParameterName = "@idEntrant",
                        Value = idEntrant
                    };
                   

                    // добавляем параметры
                    command.Parameters.Add(idParam);
                    command.Parameters.Add(idEntrantParam);
                    
                    updatted = Convert.ToBoolean(command.ExecuteNonQuery());

                    base.Close();
                    return updatted;
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show("Error: ошибка обновления записи" + "\n\n" + e.Message);
                base.Close();
                return false;
            }
            return false;
        }

        #endregion
    }
}
