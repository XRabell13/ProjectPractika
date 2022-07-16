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
                MessageBox.Show("Error: ошибка получения списка абитуриентов");
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
