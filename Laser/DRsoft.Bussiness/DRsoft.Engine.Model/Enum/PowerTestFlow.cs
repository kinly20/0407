namespace DRsoft.Engine.Model.Enum
{
    public enum PowerTestFlow
    {
        Standby,
        StartTest,//收到测试指令
        SetPara,  //设置参数
        //SetPower, //设置功率
        //JumpToZero,//移动到振镜零点
        //LaserOn,   //打开激光器
        SentCmd,   //下发指令
        WaitTime,  //等待稳定
        ReceiveData,//获取数值
        LaserOff,   //关闭激光器
        Finish

    }
}
