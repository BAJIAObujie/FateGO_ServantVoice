

//单例模式没有采用公司的模式 通过global初始化所有manager，在init中让manager持有一份自己的单例。
public interface IManager
{
	//不可以随便new 一个管理器出来 由反射生成
	
	//在Init中注册唯一实例，不可以做其他多余操作
	//比如你在从者语音Manager的Init方法里面注册了实例后继续注册从者语音的数据（这个管理器管理的数据）
	//这个时候需要从配置管理器去加载数据，但配置管理器可能还没有初始化成功呢，所以Init只注册实例。
	//start里注册其他数据
	
	void Init();

	//void Start();
}
