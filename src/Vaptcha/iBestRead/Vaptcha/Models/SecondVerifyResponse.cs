namespace iBestRead.Vaptcha.Models
{
    public class SecondVerifyResponse
    {
        public SecondVerifyResponse()
        {
        }

        public SecondVerifyResponse(VerifyResult success, string msg)
        {
            Success = success;
            Msg = msg;
        }

        public SecondVerifyResponse(VerifyResult success, int score, string msg)
        {
            Success = success;
            Score = score;
            Msg = msg;
        }

        /// <summary>
        /// 验证结果
        /// 1为通过，0为失败
        /// </summary>
        public VerifyResult Success { get; set; }

        /// <summary>
        /// 可信度，区间[0, 100]
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; } = string.Empty;
    }
}