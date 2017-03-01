using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace FEITS.Model
{
    public static class CommandTokens
    {
        #region Constants

        //TODO(Robin): Give these descriptive names
        public const string Prefix = "$";

        public const string Wa = "$Wa",
                            Wc = "$Wc",
                            a = "$a",
                            Nu = "$Nu",
                            N0 = "$N0",
                            N1 = "$N1",
                            kn = "$k\\n",
                            k = "$k",
                            t0 = "$t0",
                            t1 = "$t1",
                            p = "$p",
                            Wd = "$Wd",
                            Wv = "$Wv",
                            E = "$E",
                            Sbs = "$Sbs",
                            Svp = "$Svp",
                            Sre = "$Sre",
                            Fw = "$Fw",
                            Ws = "$Ws",
                            VF = "$VF",
                            Ssp = "$Ssp",
                            Fo = "$Fo",
                            VNMPID = "$VNMPID",
                            Fi = "$Fi",
                            b = "$b",
                            w = "$w",
                            l = "$l",
                            Wm = "$Wm",
                            Sbv = "$Sbv",
                            Sbp = "$Sbp",
                            Sls = "$Sls",
                            Slp = "$Slp",
                            Srp = "$Srp";

        #endregion

        public static bool IsValid(string token)
        {
            return AllTokensSet.Contains(token);
        }

        public static bool HasPrefix(string token)
        {
            return token.StartsWith(Prefix);
        }

        public static string WithPrefix(string token)
        {
            return HasPrefix(token) ? token : Prefix + token;
        }

        public static byte ParamsCount(string token)
        {
            if (NoParamsSet.Contains(token))
                return 0;
            if (SingleParamsSet.Contains(token))
                return 1;
            if (DoubleParamsSet.Contains(token))
                return 2;
            Debug.Assert(!AllTokensSet.Contains(token), $"{token} not found in {nameof(AllTokensSet)}");
            throw new ArgumentOutOfRangeException(nameof(token), token, "Invalid command token.");
        }

        public static IEnumerable<string> NoParams => NoParamsSet;
        public static IEnumerable<string> SingleParams => SingleParamsSet;
        public static IEnumerable<string> DoubleParams => DoubleParamsSet;

        private static readonly ISet<string>
            AllTokensSet,
            NoParamsSet = new SortedSet<string>
            {
                Wa,
                Wc,
                a,
                Nu,
                N0,
                N1,
                kn,
                k,
                t0,
                t1,
                p,
                Wd,
                Wv
            },
            SingleParamsSet = new SortedSet<string>
            {
                E,
                Sbs,
                Svp,
                Sre,
                Fw,
                Ws,
                VF,
                Ssp,
                Fo,
                VNMPID,
                Fi,
                b,
                w,
                l
            },
            DoubleParamsSet = new SortedSet<string>
            {
                Wm,
                Sbv,
                Sbp,
                Sls,
                Slp,
                Srp
            };

        static CommandTokens()
        {
            AllTokensSet = new SortedSet<string>();
            foreach (var set in new[] {NoParamsSet, SingleParamsSet, DoubleParamsSet})
            {
                foreach (var token in set)
                {
                    var result = AllTokensSet.Add(token);

                    //NOTE(Robin): Fails if duplicate token is encountered.
                    Debug.Assert(result, $"{token} successfully added to {nameof(AllTokensSet)}");
                }
            }
        }
    }
}