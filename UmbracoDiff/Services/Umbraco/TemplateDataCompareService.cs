using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using UmbracoDiff.Models;

namespace UmbracoDiff.Services.Umbraco
{
    public interface ITemplateDataCompareService : IDataCompareService<string, string>
    {
        
    }

    public class TemplateDataCompareService : IDataCompareService<string, string>, ITemplateDataCompareService
    {
        public IList<string> GetData(string connectionString)
        {
            const string queryString = @"select alias from cmsTemplate order by alias";

            var result = GetStringArrayFromQuery(connectionString, queryString);
            return result;
        }

        public DataComparissonResult<string> GetResults(string leftConnectionString, string rightConnectionString)
        {
            var leftData = GetData(leftConnectionString);
            var rightData = GetData(rightConnectionString);

            var leftResult = CompareDataSets(leftData, rightData);
            var rightResult = CompareDataSets(rightData, leftData);

            var output = new DataComparissonResult<string>
            {
                LeftResult = leftResult,
                RightResult = rightResult
            };

            return output;
        }

        private IEnumerable<string> CompareDataSets(IEnumerable<string> left, IEnumerable<string> right)
        {
            var same = from l in left
                       join r in right on l.ToLower() equals r.ToLower()
                       select l;

            var different = left.Except(same);
            return different;
        }

        private string[] GetStringArrayFromQuery(string connectionString, string query)
        {
            var result = new List<string>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        result.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            return result.ToArray();
        }
    }
}
