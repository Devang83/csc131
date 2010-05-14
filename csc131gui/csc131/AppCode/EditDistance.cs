using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuickPMWebsite.AppCode
{
    public class EditDistance
    {
        /// <summary>
        /// Compute Levenshtein distance
        /// 2 Dimensional array matrix version
        /// </summary>
        /// <returns>Levenshtein edit distance</returns>
        public static int MatrixLevenshtein(string s, string t)
        {
            int[,] matrix;          // matrix
            int n = s.Length;       // length of s
            int m = t.Length;       // length of t
            int cost;               // cost

            // Step 1
            if (n == 0) return m;
            if (m == 0) return n;

            // Create matirx
            matrix = new int[n + 1, m + 1];

            // Step 2
            // Initialize
            for (int i = 0; i <= n; i++) matrix[i, 0] = i;
            for (int j = 0; j <= m; j++) matrix[0, j] = j;

            // Step 3
            for (int i = 1; i <= n; i++)
            {

                // Step 4
                for (int j = 1; j <= m; j++)
                {

                    // Step 5
                    if (s[i - 1] == t[j - 1])
                        cost = 0;
                    else
                        cost = 1;

                    // Step 6
                    // Find minimum
                    int min = matrix[i - 1, j] + 1;
                    int b = matrix[i, j - 1] + 1;
                    int c = matrix[i - 1, j - 1] + cost;

                    if (b < min) min = b;
                    if (c < min) min = c;

                    matrix[i, j] = min;
                }
            }

            // Step 7
            return matrix[n, m];
        }

        public static List<E> SortedTenants<E>(List<E> list, string propertyName, string searchTerm)
        {
            
            if (list.Count == 0)
            {
                List<E> t = new List<E>();
                return t;
            }
            if (list.Count == 1)
            {
                List<E> t = new List<E>();
                t.Add(list[0]);
                return t;
            }
            List<E> sortedList = new List<E>();
            foreach (E o in list)
            {
                InsertObject(sortedList, o, propertyName, searchTerm);
            }            
            return sortedList;
        }

        private static void InsertObject<E>(List<E> list, E o, string propertyName, string searchTerm)
        {
            if (list.Count == 0)
            {
                list.Add(o);
                return;
            }
            for (int i = 0; i < list.Count; i++)
            {
                Type t = typeof(E);

                System.Reflection.PropertyInfo pInfo = t.GetProperty(propertyName);
                object v1 = pInfo.GetValue(list[i], null);
                object v2 = pInfo.GetValue(o, null);                    
                if (i == list.Count - 1)
                {

                    if (QuickPMWebsite.AppCode.EditDistance.MatrixLevenshtein(v1.ToString().ToLower(), searchTerm) >= QuickPMWebsite.AppCode.EditDistance.MatrixLevenshtein(v2.ToString().ToLower(), searchTerm))
                    {
                        list.Insert(i, o);
                    }
                    else
                    {
                        list.Add(o);
                    }
                    break;
                }
                if (QuickPMWebsite.AppCode.EditDistance.MatrixLevenshtein(v1.ToString().ToLower(), searchTerm) >= QuickPMWebsite.AppCode.EditDistance.MatrixLevenshtein(v2.ToString().ToLower(), searchTerm))
                {
                    list.Insert(i, o);
                    break;
                }
            }
        }



    }
}
