using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.Class
{
    /// <summary>
    /// KMP nedir ne değildir onların linkleri .
    /// </summary>
    public class KMPAlgorithm
    {
        /// <summary>
        /// verilen keyword'ü KMP algoritmasında kulalnılmak üzere hazır hale getirir.
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private int[] Prefix(string keyword)
        {

            int m = keyword.Length;
            int[] pi = new int[m];
            int k = 0;
            pi[0] = 0;

            for (int q = 1; q < m; q++)
            {
                while (k > 0 && keyword[k] != keyword[q]) { k = pi[k]; }

                if (keyword[k] == keyword[q]) { k++; }
                pi[q] = k;
            }
            return pi;
        }


        /// <summary>
        /// Verilen source içerisinde keyword'ü arar varsa true döndürür.
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="source"></param>
        /// <returns></returns>
        public Boolean Search(string keyword, string source)
        {
            int n = source.Length;
            int m = keyword.Length;

            int[] pi = Prefix(keyword);

            int q = 0;

            for (int i = 1; i <= n; i++)
            {
                while (q > 0 && !string.Equals(keyword[q].ToString(), source[i - 1].ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    q = pi[q - 1];
                }
                if (string.Equals(keyword[q].ToString(), source[i - 1].ToString(), StringComparison.CurrentCultureIgnoreCase)) { q++; }
                if (q == m)
                {

                    q = pi[q - 1];


                    return true;

                }

            }

            return false;

        }
    }
}
