// desc singleton desc class
// maintainer hugoyu

public class SingletonUtil<T> where T : class {

    static T instance;

    public static T Instance {
        
        get {
            return instance;
        }

        set {
            AssertUtil.Assert(instance == null, "Singleton has multi instances!");
            instance = value;
        }
    }

}

