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

namespace ProjectPractika.DataBase
{
    public class DBLoad : Connect
    {

        public ObservableCollection<Models.Category> GetAllCategory()
        {
            ObservableCollection<Models.Category> categories = new ObservableCollection<Models.Category>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllCategorySpec";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        categories.Add(new Models.Category(reader.GetInt32(0), reader.GetString(1)));
                }
                reader.Close();
                base.Close();
                return categories;
            }
            else
            {
                System.Windows.MessageBox.Show("Error: ошибка получения списка категорий");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<Models.Specialization> GetAllSpecializations()
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

        public ObservableCollection<SpecializationDG> GetAllSpecializationsWithCategory()
        {
            ObservableCollection<SpecializationDG> specializations = new ObservableCollection<SpecializationDG>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllSpecializationWithCategory";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        specializations.Add(new SpecializationDG(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
                base.Close();
                return specializations;
            }
            else
            {
                System.Windows.MessageBox.Show("Error: ошибка получения списка специальностей с категориями");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<SpecializationDG> GetAllSpecializationsWithCategory(string specName)
        {
            ObservableCollection<SpecializationDG> specializations = new ObservableCollection<SpecializationDG>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllSpecializationWithCategoryBySpecName";
                SqlCommand command = new SqlCommand(sqlExpression, connection);


                SqlParameter specNameParam = new SqlParameter
                {
                    ParameterName = "@specName",
                    Value = specName
                };
                // добавляем параметры
                command.Parameters.Add(specNameParam);

                command.CommandType = System.Data.CommandType.StoredProcedure;
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        specializations.Add(new SpecializationDG(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                }
                reader.Close();
                base.Close();
                return specializations;
            }
            else
            {
                System.Windows.MessageBox.Show("Error: ошибка получения списка специальностей с категориями");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<Models.Specialization> GetAllSpecializationsBasic(string specName)
        {
            ObservableCollection<Models.Specialization> specializations = new ObservableCollection<Models.Specialization>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchSpecializationBasic";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметры для ввода 
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

        public ObservableCollection<EducationIns> GetAllEducationalInsByName(string eduName)
        {
            ObservableCollection<EducationIns> educationals = new ObservableCollection<EducationIns>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchEducationalInsByName";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@eduName",
                    Value = eduName
                };
                // добавляем параметры
                command.Parameters.Add(nameParam);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                        educationals.Add(new EducationIns(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    // MessageBox.Show(educationals[0].ToString());
                }
                reader.Close();
                base.Close();
                return educationals;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка абитуриентов");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<EducationIns> GetAllEducationalIns()
        {
            ObservableCollection<EducationIns> educationals = new ObservableCollection<EducationIns>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllEducationalIns";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;


                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                        educationals.Add(new EducationIns(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    // MessageBox.Show(educationals[0].ToString());
                }
                reader.Close();
                base.Close();
                return educationals;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка абитуриентов");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<ConcourseWithEduAndSpec> GetAllConcourseWithEduAndSpec(int year, int isFree, int isIntramural, string specName)
        {
            ObservableCollection<ConcourseWithEduAndSpec> councourses = new ObservableCollection<ConcourseWithEduAndSpec>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchConcourseByInfo";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
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
                SqlParameter specNameParam = new SqlParameter
                {
                    ParameterName = "@specName",
                    Value = specName
                };
                // добавляем параметры
                command.Parameters.Add(yearParam);
                command.Parameters.Add(isFreeParam);
                command.Parameters.Add(isIntramuralParam);
                command.Parameters.Add(specNameParam);


                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        councourses.Add(new ConcourseWithEduAndSpec(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                   
                }
                reader.Close();
                base.Close();
                return councourses;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка конкурсов");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<ConcourseWithEduAndSpec> GetAllConcourseWithInfo(string specName)
        {
            ObservableCollection<ConcourseWithEduAndSpec> councourses = new ObservableCollection<ConcourseWithEduAndSpec>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchConcourseWithInfo";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
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
                        councourses.Add(new ConcourseWithEduAndSpec(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetBoolean(3), reader.GetBoolean(4), reader.GetInt32(5)));

                }
                reader.Close();
                base.Close();
                return councourses;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<ConcourseWithEduAndSpec> GetAllConcourseWithInfoDG(string specName)
        {
            ObservableCollection<ConcourseWithEduAndSpec> councourses = new ObservableCollection<ConcourseWithEduAndSpec>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllConcourseWithInfoDGBySpecName";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
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
                        councourses.Add(new ConcourseWithEduAndSpec(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetBoolean(3), reader.GetBoolean(4), reader.GetInt32(5), reader.GetInt32(6)));

                }
                reader.Close();
                base.Close();
                return councourses;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<ConcourseWithEduAndSpec> GetAllConcourseWithInfo()
        {
            ObservableCollection<ConcourseWithEduAndSpec> councourses = new ObservableCollection<ConcourseWithEduAndSpec>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllConcourseWithInfoDG";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                        councourses.Add(new ConcourseWithEduAndSpec(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetBoolean(3), reader.GetBoolean(4), reader.GetInt32(5),reader.GetInt32(6)));

                }
                reader.Close();
                base.Close();
                return councourses;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                base.Close();
                return null;
            }
        }


        public ObservableCollection<ConcoursesForView> GetAllConcoursesForViewByLetterAndYear(char letter, int year)
        {
            ObservableCollection<ConcoursesForView> councourses = new ObservableCollection<ConcoursesForView>();
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "GeneralApp.AllConcourseForViewWithInfoByLetterAndYear";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter nameParam = new SqlParameter
                    {
                        ParameterName = "@letter",
                        Value = letter
                    };
                    SqlParameter yearParam = new SqlParameter
                    {
                        ParameterName = "@year",
                        Value = year
                    };
                    // добавляем параметры
                    command.Parameters.Add(nameParam);
                    command.Parameters.Add(yearParam);

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            councourses.Add(new ConcoursesForView(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                  reader.GetString(3), reader.GetBoolean(4), reader.GetBoolean(5),
                                  reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9)));

                    }
                    reader.Close();
                    base.Close();
                    return councourses;
                }
                else
                {
                    MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                    base.Close();
                    return null;
                }
            }
            catch(Exception e) { MessageBox.Show(e.Message); }
            return null;
        }

        public ObservableCollection<ConcoursesForView> GetAllConcoursesForViewByInfo(int categoryId, int isFree, int isIntramural)
        {
            ObservableCollection<ConcoursesForView> councourses = new ObservableCollection<ConcoursesForView>();
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "GeneralApp.AllConcourseForViewWithInfoByInfo";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
                    SqlParameter catIdParam = new SqlParameter
                    {
                        ParameterName = "@categoryId",
                        Value = categoryId
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
                    command.Parameters.Add(catIdParam);
                    command.Parameters.Add(isFreeParam);
                    command.Parameters.Add(isIntramuralParam);

                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                            councourses.Add(new ConcoursesForView(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                  reader.GetString(3), reader.GetBoolean(4), reader.GetBoolean(5),
                                  reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9)));

                    }
                    reader.Close();
                    base.Close();
                    return councourses;
                }
                else
                {
                    MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                    base.Close();
                    return null;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            return null;
        }

        public ObservableCollection<ConcoursesForView> GetAllConcoursesForViewBySpecName(string specName)
        {
            ObservableCollection<ConcoursesForView> councourses = new ObservableCollection<ConcoursesForView>();
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "GeneralApp.AllConcourseForViewWithInfoBySpecName";
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // параметры для ввода 
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
                            councourses.Add(new ConcoursesForView(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                  reader.GetString(3), reader.GetBoolean(4), reader.GetBoolean(5),
                                  reader.GetInt32(6), reader.GetInt32(7), reader.GetInt32(8), reader.GetInt32(9)));

                    }
                    reader.Close();
                    base.Close();
                    return councourses;
                }
                else
                {
                    MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                    base.Close();
                    return null;
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            return null;
        }

        public ObservableCollection<SpecializationWithInfo> GetAllSpecializations(string specName)
        {
            ObservableCollection<SpecializationWithInfo> specializations = new ObservableCollection<SpecializationWithInfo>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SearchSpecializationByInfo";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
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
                        specializations.Add(new SpecializationWithInfo(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                   

                }
                reader.Close();
                base.Close();
                return specializations;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка абитуриентов");
                base.Close();
                return null;
            }
        }

        public ObservableCollection<Specialization> GetAllSpecializationsByName(string specName)
        {
            ObservableCollection<Specialization> specializations = new ObservableCollection<Specialization>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.SpecializationByName";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
                SqlParameter specNameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = specName
                };

                // добавляем параметры
                command.Parameters.Add(specNameParam);


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
                MessageBox.Show("Error: ошибка получения списка абитуриентов");
                base.Close();
                return null;
            }
        }

        public List<int> GetBallsByConcourse(int idConcourse)
        {
            List<int> balls = new List<int>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.GetBallsByConcourse";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // параметры для ввода 
                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@conId",
                    Value =idConcourse
                };

                // добавляем параметры
                command.Parameters.Add(idParam);


                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                       balls.Add(reader.GetInt32(0));

                }
                reader.Close();
                base.Close();
                return balls;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка баллов");
                base.Close();
                return null;
            }
        }
        //пагинация в datagrid

        public ObservableCollection<EducationIns> GetAllEduInsPagination(int offset, int limit)
        {
            ObservableCollection<EducationIns> eduIns = new ObservableCollection<EducationIns>();
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "GeneralApp.AllEducationalInsPagination";
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
                            eduIns.Add(new EducationIns(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }
                    reader.Close();
                    base.Close();
                    return eduIns;
                }
                else
                {
                    MessageBox.Show("Error: ошибка получения списка учреждений образования");
                    base.Close();
                    return null;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: ошибка получения списка учреждений образования" + "\n\n" + e.Message);
            }
            return null;

        }

        public ObservableCollection<SpecializationDG> GetAllSpecializationPagination(int offset, int limit)
        {
            ObservableCollection<SpecializationDG> eduIns = new ObservableCollection<SpecializationDG>();
            Open();
            try
            {
                if (status)
                {
                    string sqlExpression = "GeneralApp.AllSpecializationPagination";
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
                            eduIns.Add(new SpecializationDG(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    }
                    reader.Close();
                    base.Close();
                    return eduIns;
                }
                else
                {
                    MessageBox.Show("Error: ошибка получения списка специальностей");
                    base.Close();
                    return null;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("Error: ошибка получения списка специальностей" + "\n\n" + e.Message);
            }
            return null;

        }

        public ObservableCollection<ConcourseWithEduAndSpec> GetAllConcourseWithInfoPagination(int offset, int limit)
        {
            ObservableCollection<ConcourseWithEduAndSpec> councourses = new ObservableCollection<ConcourseWithEduAndSpec>();
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.AllConcourseDGPagination";
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
                        councourses.Add(new ConcourseWithEduAndSpec(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                            reader.GetBoolean(3), reader.GetBoolean(4), reader.GetInt32(5), reader.GetInt32(6)));

                }
                reader.Close();
                base.Close();
                return councourses;
            }
            else
            {
                MessageBox.Show("Error: ошибка получения списка конкурсов с расширенной информацией");
                base.Close();
                return null;
            }
        }

        // Проверка праивльности логина и пароля
        public bool IsAdminCheck(string login, string password)
        { 
            bool isAdmin = false;
            Open();
            if (status)
            {
                string sqlExpression = "GeneralApp.IsAdminCheck";
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметры для ввода 
                SqlParameter loginParam = new SqlParameter
                {
                    ParameterName = "@login",
                    Value = login
                };
                SqlParameter passParam = new SqlParameter
                {
                    ParameterName = "@password",
                    Value = password
                };
                // добавляем параметры
                command.Parameters.Add(loginParam);
                command.Parameters.Add(passParam);

                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    System.Windows.MessageBox.Show("Верно");
                    isAdmin = true;
                    reader.Close();
                    base.Close();
                    return isAdmin;
                }
                System.Windows.MessageBox.Show("Не Верно");
                reader.Close();
                base.Close();
                return isAdmin;
            }
            else
            {
                System.Windows.MessageBox.Show("Error: ошибка проверки логина и пароля");
                base.Close();
                return isAdmin;
            }
        }

    }
}
