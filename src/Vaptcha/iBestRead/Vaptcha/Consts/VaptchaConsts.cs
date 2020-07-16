namespace iBestRead.Vaptcha.Consts
{
    public static class VaptchaConsts
    {
        /// <summary>
        /// default http client name
        /// </summary>
        public const string HttpClientName = "VaptchaClient";
        
        /// <summary>
        /// 二次验证Url
        /// 参考 https://www.vaptcha.com/document/install.html#%E6%9C%8D%E5%8A%A1%E7%AB%AF%E4%BA%8C%E6%AC%A1%E9%AA%8C%E8%AF%81
        /// </summary>
        public const string SecondVerifyUrl = "https://0.vaptcha.com/verify";
        
        // 二次验证
        public const string OfflineMode = "offline";
            
    }
}