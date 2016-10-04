using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_sharp
{
    abstract class Example
    {
        public void process()
        {
            Console.WriteLine("+-----------------" + this.ToString() +"-----------------+");
            Console.WriteLine(Title);
            processExample();
            Console.WriteLine("+--------------------------------------------------+\n\n");
        }

        public virtual string Title { get { return ""; } }
        public abstract void processExample();
    }

    class Example1 : Example
    {
        public override string Title { get { return "stack"; } }

        public override void processExample()
        {
            Stack<int> st = new Stack<int>();

            st.Push(1);
            st.Push(2);
            st.Push(3);

            Console.WriteLine(st.Pop());
            Console.WriteLine(st.Pop());
            Console.WriteLine(st.Pop());
        }
    }

    class Example2 : Example
    {
        struct Data
        {
            public Data(int a, float b, string c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

            public void print()
            {
                Console.WriteLine(a + " " + b + " " + c);
            }

            public int a;
            public float b;
            public string c;
        }

        public override string Title { get { return "call by value with struct"; } }

        public override void processExample()
        {
            Data data1 = new Data(1, 2, "none");
            Data data2 = data1;
            data2.c = "changed";

            data1.print();
            data2.print();
        }
    }

    class Example3 : Example
    {
        class Data
        {
            public Data(int a, float b, string c)
            {
                this.a = a;
                this.b = b;
                this.c = c;
            }

            public void print()
            {
                Console.WriteLine(a + " " + b + " " + c);
            }

            public int a;
            public float b;
            public string c;
        }

        public override string Title { get { return "call by reference with class"; } }

        public override void processExample()
        {
            Data data1 = new Data(1, 2, "none");
            Data data2 = data1;
            data2.c = "changed";

            data1.print();
            data2.print();
        }
    }

    class Example4 : Example
    {
        public override string Title { get { return "boxing unboxing"; } }

        public override void processExample()
        {
            int a = 10;
            object o = a;   // boxing   (인스턴스 할당 + 객체에 값 복사) 
            int b = (int)o; // unboxing (올바른 값인지 검사 + 값 복사)
        }
    }

    class Example5 : Example
    {
        public override string Title { get { return "indexer"; } }

        class MyMap
        {
            public int Length
            { 
                get { return _length; }
            }

            public string this[int index]
            {
                get
                {
                    foreach (var d in _data)
                        if (d.Key == index)
                            return d.Value;
                    return null;
                }

                set
                {
                    bool found = false;
                    foreach (var d in _data)
                        if (d.Key == index)
                            found = true;

                    if (!found)
                        _data[_length++] = new KeyValuePair<int, string>(index, value);
                }
            }

            public MyMap()
            {
                _data = new KeyValuePair<int, string>[100];
                _length = 0;
            }

            private KeyValuePair<int, string>[] _data;
            private int _length;
        }


        public override void processExample()
        {
            MyMap map = new MyMap();

            Console.WriteLine(map.Length);

            map[14] = "fantasy";
            map[99] = "susuba";

            Console.WriteLine(map.Length + " " + map[14] + " " + map[99]);
        }
    }

    class Example6 : Example
    {
        public override string Title { get { return "delegate with sort"; } }

        
        delegate bool Strategy<T>(T a, T b);

        static bool Greater(int a, int b)
        {
            return a > b;
        }

        static bool Less(int a, int b)
        {
            return a < b;
        }

        static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        static void Sort<T>(T[] arr, Strategy<T> strategy)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                for (int j = 0; j < arr.Length - 1 - i; j++)
                {
                    if (strategy(arr[j], arr[j + 1]))
                    {
                        Swap(ref arr[j], ref arr[j + 1]);
                    }
                }
            }
        }
        
        public override void processExample()
        {
            int[] arr = new int[10] { 3, 2, 1, 8, 5, 10, 7, 4, 6, 9 };

            Sort(arr, Less);
            foreach (var d in arr)
                Console.Write(d + " ");
            Console.WriteLine();

            Sort(arr, Greater);
            foreach (var d in arr)
                Console.Write(d + " ");
            Console.WriteLine();

            Sort(arr, (int a, int b) => { return a < b; });
            foreach (var d in arr)
                Console.Write(d + " ");
            Console.WriteLine();

            Sort(arr, (int a, int b)=>{ return a > b; });
            foreach (var d in arr)
                Console.Write(d + " ");
            Console.WriteLine();
        }
    }

    class Example7 : Example
    {
        public override string Title { get { return "event with observer pattern"; } }

        public class Observee
        {
            public delegate void TriggerType();

            public enum StateType
            {
                Idle, Walk, Attack
            }
            
            public StateType State
            {
                get { return _state; }
                set
                {
                    _state = value;
                    if(_trigger != null)
                        _trigger();
                }
            }

            public event TriggerType Trigger
            {
                add
                {
                    _trigger += value;
                }
                remove
                {
                    _trigger -= value;
                }
            }

            public Observee()
            {
                _state = StateType.Idle;
                _trigger += ActionOnStateChange;
            }

            public void ActionOnStateChange()
            {
                Console.WriteLine("State changed to : " + _state);
            }

            private StateType _state;
            private event TriggerType _trigger;
        }
       

        public override void processExample()
        {
            Observee ob = new Observee();
            ob.State = Observee.StateType.Attack;
        }
    }

    class Example8 : Example
    {
        public override string Title { get { return "operator overloading"; } }

        class Temp
        {
            public Temp(int a, float b, string c)
            {
                _a = a;
                _b = b;
                _c = c; 
            }
            private int _a;
            private float _b;
            private string _c;

            public static bool operator ==(Temp l, Temp r)
            {
                return l._a == r._a && l._b == r._b && l._c == r._c;
            }

            public static bool operator !=(Temp l, Temp r)
            {
                return !(l == r);
            }
        }
        
        public override void processExample()
        {
            Temp a = new Temp(1, 5.4f, "fadf");
            Temp b = new Temp(2, 5.4f, "fadf");
            Temp c = new Temp(1, 5.4f, "fadf");

            Console.WriteLine(a == b);
            Console.WriteLine(b == c);
            Console.WriteLine(c == a);
        }
    }

    class Example9 : Example
    {
        public override string Title { get { return "interface with class composition"; } }

        interface ISound
        {
            SoundSystem SoundSys { get; set; }
        }

        interface IRendering
        {
            RenderingSystem RenderingSys { get; set; }
        }

        interface IColliding
        {
            CollisionSystem CollisionSys { get; set; }
        }

        interface IMovable
        {
            MovingSystem MovingSys { get; set; }
        }

        interface IMessageHandling
        {
            MessageHandlingSystem MessageHandlingSys { get; set; }
        }

        class SoundSystem
        {
            public void Play() { Console.WriteLine("play sound"); }
        }

        class RenderingSystem
        {
            public void Render() { Console.WriteLine("render image"); }
        }

        class CollisionSystem
        {
            public void CheckCollision() { Console.WriteLine("check collision"); }
        }

        class MovingSystem
        {
            public void Move() { Console.WriteLine("move"); }
        }

        class MessageHandlingSystem
        {
            public void HandleMessage() { Console.WriteLine("handle message"); }
            public void SendMessage() { Console.WriteLine("send message"); }
        }

        abstract class Entity
        {
            public uint ID
            {
                get { return _id; }
                set { _id = value; }
            }

            public Entity(uint id)
            {
                _id = id;
            }

            public void Print()
            {
                Console.WriteLine("entity id : " + _id);
                PrintData();
                Console.WriteLine();
            }

            protected abstract void PrintData();

            private uint _id;
        }

        class Entity1 : Entity, ISound, IRendering
        {
            public SoundSystem SoundSys
            {
                get { return _soundSys; }
                set { _soundSys = value; }
            }

            public RenderingSystem RenderingSys
            {
                get { return _renderingSys; }
                set { _renderingSys = value; }
            }

            public Entity1(uint id)
                :
                base(id)
            {
                _soundSys = new SoundSystem();
                _renderingSys = new RenderingSystem();
            }

            protected override void PrintData()
            {
                _soundSys.Play();
                _renderingSys.Render();
            }

            private SoundSystem _soundSys;
            private RenderingSystem _renderingSys;
        }

        class Entity2 : Entity, ISound, IRendering, IColliding, IMessageHandling
        {
            public SoundSystem SoundSys
            {
                get { return _soundSys; }
                set { _soundSys = value; }
            }

            public RenderingSystem RenderingSys
            {
                get { return _renderingSys; }
                set { _renderingSys = value; }
            }

            public MessageHandlingSystem MessageHandlingSys
            {
                get { return _messageHandlingSys; }
                set { _messageHandlingSys = value; }
            }

            public CollisionSystem CollisionSys
            {
                get { return _collisionSys; }
                set { _collisionSys = value; }
            }

            public Entity2(uint id)
                :
                base(id)
            {
                _soundSys = new SoundSystem();
                _renderingSys = new RenderingSystem();
                _collisionSys = new CollisionSystem();
                _messageHandlingSys = new MessageHandlingSystem();
            }

            protected override void PrintData()
            {
                _soundSys.Play();
                _renderingSys.Render();
                _collisionSys.CheckCollision();
                _messageHandlingSys.HandleMessage();
                _messageHandlingSys.SendMessage();
            }

            private SoundSystem _soundSys;
            private RenderingSystem _renderingSys;
            private CollisionSystem _collisionSys;
            private MessageHandlingSystem _messageHandlingSys;
        }

        class Entity3 : Entity, ISound, IRendering, IMovable
        {
            public SoundSystem SoundSys
            {
                get { return _soundSys; }
                set { _soundSys = value; }
            }

            public RenderingSystem RenderingSys
            {
                get { return _renderingSys; }
                set { _renderingSys = value; }
            }

            public MovingSystem MovingSys
            {
                get { return _movingSys; }
                set { _movingSys = value; }
            }

            public Entity3(uint id)
                :
                base(id)
            {
                _soundSys = new SoundSystem();
                _renderingSys = new RenderingSystem();
                _movingSys = new MovingSystem();
            }

            protected override void PrintData()
            {
                _soundSys.Play();
                _renderingSys.Render();
                _movingSys.Move();
            }

            private SoundSystem _soundSys;
            private RenderingSystem _renderingSys;
            private MovingSystem _movingSys;
        }


        public override void processExample()
        {
            Entity[] entities = new Entity[3];

            entities[0] = new Entity1(1);
            entities[1] = new Entity2(2);
            entities[2] = new Entity3(1);

            foreach (var e in entities)
            {
                e.Print();
            }
        }
    }

    class Example10 : Example
    {
        public override string Title { get { return "c# component pattern1"; } }
        
        public override void processExample()
        {
            
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Example[] examples = new Example[]
            {
                new Example1(),
                new Example2(),
                new Example3(),
                new Example4(),
                new Example5(),
                new Example6(),
                new Example7(),
                new Example8(),
                new Example9(),
                new Example10(),
            };

            foreach (var e in examples)
                e.process();
            
        }
    }
}
