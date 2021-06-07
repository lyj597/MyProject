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

        private static BaseClass _base = new BaseClass() { Name="中华人民共和国"};

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

    public abstract class TenXun
    {
        public List<IObservable> _observables = new List<IObservable>();

        public string Info { get; set; }

        public TenXun(string _Info) {
            Info = _Info;
        }

        public void AddObservable(IObservable observable) {
            _observables.Add(observable);
        }

        public void RemoveObservable(IObservable observable)
        {
            _observables.Remove(observable);
        }

        public void Update() {
            if (_observables != null) {
                foreach (var o in _observables) {
                    o.RecieveMessage(this);
                }
                    
            }
        }
    }

    public interface IObservable
    {
        public void RecieveMessage(TenXun tenXun);
    }

     // 具体订阅号类
     public class TenXunGame : TenXun
     {
         public TenXunGame(string info)
             : base(info) 
         { 
         }
    }

    // 具体的订阅者类
     public class Subscriber : IObservable
    {
         public string Name { get; set; }
         public Subscriber(string name)
         {
             this.Name = name;
         }       

        public void RecieveMessage(TenXun tenXun)
        {
            Console.WriteLine($"Name:{Name} Info is: {tenXun.Info}");
        }
    }

    public delegate void EventHandler();

    public class Cat {
      public EventHandler eventHandler;

        public Cat() {
            eventHandler += new EventHandler(Mouse.Escape);
            eventHandler += new EventHandler(Dog.Chase);
        }
      public  void Call() {
          Console.WriteLine("猫叫了");
            Update();
      }

        private void Update() {
            if (eventHandler != null) {
                eventHandler();
            }
        }
    }
    public  class Mouse
    {
        public static void Escape()
        {
            Console.WriteLine("老鼠逃跑了");
        }
    }

    public  class Dog
    {
        public static void Chase()
        {
            Console.WriteLine("狗追了");
        }
    }


    #endregion
}
