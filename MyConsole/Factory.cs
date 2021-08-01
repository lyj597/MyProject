using System;
using System.Collections.Generic;
using System.Text;

namespace MyConsole
{
    #region 单利模式
    public class Singleton
    {
        private Singleton() { }
        private static object _lock = new object();

        public static Singleton _singleton = null;

        public static Singleton CreateSingleton()
        {
            Console.WriteLine("路过");
            if (_singleton == null)
            {
                lock (_lock)
                {
                    Console.WriteLine("创建");
                    _singleton = new Singleton();
                }

            }
            return _singleton;
        }
    }

    public class SingleOne {
        private SingleOne() { }

        public static SingleOne singleInstance = new SingleOne();

        public SingleOne getInstance {
            get {
                return singleInstance;
            }
        }

    }

    public class SingletonSecond
    {
        public static SingletonSecond _singleton = null;
        static SingletonSecond()
        {
            _singleton = new SingletonSecond();
        }

        public static SingletonSecond CreateInstance()
        {
            return _singleton;
        }
    }

    public class SingleTest {
        private SingleTest() { }

        private static BaseClass _base = new BaseClass() { Name = "中华人民共和国" };

        public static BaseClass GetBase {
            get {
                return _base;
            }
        }
    }

    public class BaseClass
    {
        public string Name { get; set; }
    }
    #endregion

    #region   工厂模式
    public class Car {
        public string Title { get; set; }
    }

    public class DaZhongCar : Car {
    }
    public class BaoMaCar : Car
    {
    }

    public class CarFactory {
        public static DaZhongCar CreateDaZhongCar() {
            var car = new DaZhongCar() { Title = "大众汽车" };
            return car;
        }
        public static DaZhongCar CreateBaoMaCar()
        {
            var car = new DaZhongCar() { Title = "宝马汽车" };
            return car;
        }

        public static Car CreateCar(Car car) {

            return car;
        }
    }

    #endregion

    #region 观察者模式

    public abstract class TenXun{
        List<IObserverable> observables = new List<IObserverable>();

         public string Info { get; set; }
        public TenXun(string info) {
            this.Info = info;
        }

        public void AddObserverable(IObserverable observerable) {
            observables.Add(observerable);
        }

        public void RemoveObserverable(IObserverable observerable)
        {
            observables.Add(observerable);
        }

        public void Update() {
            if (observables != null && observables.Count > 0) {
                foreach (var o in observables)
                    o.RecieveMssgage(this);
            }
        }
    }

    public interface IObserverable {
        void RecieveMssgage(TenXun tenXun);
    }

    public class TenXunUpdate : TenXun {
        public TenXunUpdate(string info) : base(info) { }

    }

    public class Observerable : IObserverable
    {
        public string Name { get; set; }

        public Observerable(string name) {
            this.Name = name;
        }
        public void RecieveMssgage(TenXun tenXun)
        {
            Console.WriteLine($"HI {this.Name } 腾讯更新了{tenXun.Info},请及时更新!");
        }
    }



    public class Cat {
        public event Action CatEvent;
        public string Name { get; set; }

        public Cat(string name) {
            this.Name = name;
        }

        public void CatComeIn() {
            Console.WriteLine($"猫{this.Name}进来了");
            if (CatEvent != null)
            {
                CatEvent();
            }
        }
    }

    public class Mouse
    {
        public string Name { get; set; }

        public Mouse(string name,Cat cat)
        {
            this.Name = name;
            cat.CatEvent += MouseRunAway;
        }

        public void MouseRunAway()
        {
            Console.WriteLine($"老鼠{this.Name}逃跑了");
        }
    }

    public class Dog
    {
        public string Name { get; set; }

        public Dog(string name, Cat cat)
        {
            this.Name = name;
            cat.CatEvent += DogChase;
        }

        public void DogChase()
        {
            Console.WriteLine($"狗{this.Name}追了");
        }
    }

    #endregion
}
