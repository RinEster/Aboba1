using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace Aboba
{

           //    static void Main()
        //    {
        //        User Alice = new User();
        //        Alice.id = 0;
        //        Alice.preferences = new int[] {1, 0, 0, 1};

        //        User Bob = new User();
        //        Bob.id = 1;
        //        Bob.preferences = new int[] { 0, 1, 1, 0 };

        //        User Candice = new User();
        //        Candice.id = 2;
        //        Candice.preferences = new int[] { 1, 1, 1, 0 };

        //        User David = new User();
        //        David.id = 3;
        //        David.preferences = new int[] { 1, 1, 1, 0 };

        //        var a= David.recommendUsers(David, new User[] { Alice, Bob, Candice, David});
        //        foreach (var item in a) {
        //            System.Console.WriteLine(item);
        //        }
        //    }
        //}


        public class Wrapper
        {
            public int Primary { get; init; }
            public int Secondary { get; init; }
        }

        public class User //минимальные требования для работы функции рекомендации, сам класс переделайте под БД
        {
            public int id;
            public int[] preferences;

            private static int SameAnswers(int[] a, int[] b)
            {
                int d = 0;
                for (int i = 0; i < a.Length; ++i)
                {
                    if (a[i] == b[i]) { d++; }
                }
                return d;
            }

            public int[] recommendUsers(User user, User[] users)
            {
                int[][] prefOfUsers = new int[users.Length][];
                int j = 0;
                foreach (User u in users)
                {
                    if (u.id == user.id) continue;
                    prefOfUsers[j] = new int[] { SameAnswers(user.preferences, u.preferences), u.id };
                    ++j;
                }

                Wrapper[] prefOfUsers1 = new Wrapper[users.Length - 1];

                for (int i = 0; i < users.Length - 1; ++i)
                {
                    prefOfUsers1[i] = new() { Primary = prefOfUsers[i][0], Secondary = prefOfUsers[i][1] };
                    //prefOfUsers1[i].Primary = prefOfUsers1[0];
                    //prefOfUsers1[i].Secondary = prefOfUsers1[1];
                }

                var sorted = prefOfUsers1.OrderBy(p => p.Primary)
                     .ThenBy(p => p.Secondary);

                //.OrderBy(p => p.Primary);
                //List<int> prefOfUsers1 = prefOfUsers.OrderBy(prefOfUsers => prefOfUsers[0]);
                int[] prefOfUsers2 = new int[users.Length - 1];

                int k = 0;
                foreach (var u in sorted)
                {
                    prefOfUsers2[k] = u.Secondary;
                    ++k;
                }
                prefOfUsers2 = prefOfUsers2.Reverse().ToArray();
                return prefOfUsers2;
            }
        }
}

