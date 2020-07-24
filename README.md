# WcfTest
WCF  多通道 多服务 统一异常处理

1.多服务时，地址不要重复

2.若普通异常不做转换（通道会被堵塞）需要利用TryCatch 或ErrorHandle做异常处理=》WCF的异常

3.CallBack 模式下，若客户端处理时未加异常，会导致通道中断，Chanel关闭不了，注意=》客户端回调用Try Catch处理异常，防止通道被中断！

4.以上适合长连接和短连接的情况

5.单独独立出了短连接（即每次调用开启通道），调用结束自动关闭/发生异常自动Abort的方法=》放在ReadlProx文件夹下，使用的时候，在ClientSystem中添加
各个需要连接的终节点（节点由AppConfig中配置）

注意：1.BatchingHosting 为服务端，2.客户测试端为：ConsoleApplicationClient 
	3.DirectProxyClient 这种调用Service的方式不推荐 