#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class AppleTangle
    {
        private static byte[] data = System.Convert.FromBase64String("bFtSV19QXVseUVAeSlZXTR5dW0yVnU+seW1r/5ERf43Gxd1O89idcr4qFe5XeapIN8DKVbMQfpjJeXNBOD1rIzA6KDoqFe5XeapIN8DKVbOroEQymnm1ZeooCQ31+jFz8CpX7znSQwe9tW0e7Qb6j4GkcTRVwRXCWbE2ih7J9ZISHlFOiAE/DrKJffEYDho4PWs6NS0jf05OUlsefVtMSkpWUUxXSkcPKA4qOD1rOj0tM39OGtzV74lO4TF73xn0z1NG09mLKSlOUlsebFFRSh59fw4gKTMOCA4KDAsMDwoODQhkKTMNCw4MDgcMDwoOd+ZIoQ0qW59JqvcTPD0/Pj+dvD86OC08a20PLQ4vOD1rOjQtNH9OTlJbHndQXRAPGA4aOD1rOjUtI39OOA4xOD1rIy0/P8E6Ow49Pz/BDiM2FTg/Ozs5PD8oIFZKSk5NBBERSREOv/04NhU4Pzs7OTw8Dr+IJL+NsU2/XvglZTcRrIzGenbOXgagK8taCx0rdStnI42qyciioPFuhP9mbigOKjg9azo9LTN/Tk5SWx5sUVFKHl9QWh5dW0xKV1hXXV9KV1FQHk5EDrw/SA4wOD1rIzE/P8E6Oj08PzZgDrw/Lzg9ayMeOrw/Ng68PzoOXFJbHk1KX1BaX0xaHkpbTFNNHl+Ayk2l0OxaMfVHcQrmnADHRsFV9kceX01NS1NbTR5fXV1bTkpfUF1bOz49vD8xPg68PzQ8vD8/PtqvlzfnCEH/uWvnmaeHDHzF5utPoECfbGeZOzdCKX5oLyBK7Ym1HQV5netRMzg3FLh2uMkzPz87Oz49vD8/PmKW4kAcC/Qb6+cx6FXqnBodL8mfklBaHl1RUFpXSldRUE0eUVgeS01bCKdyE0aJ07Kl4s1JpcxI7EkOcf+LBJPKMTA+rDWPHygQSusCM+VcKI8OZtJkOgyyVo2xI+BbTcFZYFuCTlJbHn1bTEpXWFddX0pXUVAef0uJJYOtfBosFPkxI4hzomBd9nW+KQ0IZA5cDzUONzg9azo4LTxrbQ8tHlFYHkpWWx5KVltQHl9OTlJXXV9KV1hXXV9KWx5cRx5fUEceTl9MSkF/lqbH7/RYohpVL+6dhdolFP0hEh5dW0xKV1hXXV9KWx5OUVJXXUchu727JacDeQnMl6V+sBLqj64s5v5dDUnJBDkSaNXkMR8w5IRNJ3GLAxhZHrQNVMkzvPHg1Z0Rx21UZVoQfpjJeXNBNmAOITg9ayMdOiYOKA4vOD1rOjQtNH9OTlJbHndQXRAPHn1/Drw/HA4zODcUuHa4yTM/Pz97QCFyVW6of7f6Slw1Lr1/uQ20v7w/Pjg3FLh2uMldWjs/Dr/MDhQ4TF9dSlddWx5NSl9KW1NbUEpNEA4hr+UgeW7VO9NgR7oT1QicaXJr0hS4drjJMz8/Ozs+DlwPNQ43OD1r9ydMy2Mw60FhpcwbPYRrsXNjM8+1J7fgx3VSyzmVHA481iYAxm437UlJEF9OTlJbEF1RUxFfTk5SW11fV1hXXV9KV1FQHn9LSlZRTFdKRw8OvDqFDrw9nZ49PD88PD88DjM4NzGjA80VdxYk9sDwi4cw52Ai6PUDbpS06+Tawu43OQmOS0sf");
        private static int[] order = new int[] { 38,24,32,30,18,51,21,10,51,53,21,21,27,32,30,29,35,37,55,20,27,45,28,33,42,50,40,30,58,52,47,54,43,48,48,40,55,59,51,41,41,59,44,49,57,56,54,53,49,50,50,54,52,57,57,59,59,59,59,59,60 };
        private static int key = 62;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
#endif
