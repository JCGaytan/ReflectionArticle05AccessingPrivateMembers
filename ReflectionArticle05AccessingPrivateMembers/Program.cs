using System.Reflection;

namespace ReflectionArticle05AccessingPrivateMembers
{
    /// <summary>
    /// A sample class to demonstrate reflection concepts.
    /// </summary>
    class MyClass
    {
        private int privateField = 42;
        private string privateProperty = "Initial Value";

        private void PrivateMethod()
        {
            Console.WriteLine("PrivateMethod() called.");
        }

        private void PrivateMethodWithParameter(string message)
        {
            Console.WriteLine($"PrivateMethodWithParameter() called with message: {message}");
        }

        private string PrivateProperty
        {
            get { return privateProperty; }
            set { privateProperty = value; }
        }
    }

    /// <summary>
    /// Main program class for the reflection example.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Creating objects using reflection
            Type classType = typeof(MyClass);
            object instance = Activator.CreateInstance(classType);

            Console.WriteLine("Object created using reflection.");

            // Bypassing access modifiers to access private members
            FieldInfo privateFieldInfo = classType.GetField("privateField", BindingFlags.NonPublic | BindingFlags.Instance);
            if (privateFieldInfo != null)
            {
                int value = (int)privateFieldInfo.GetValue(instance);
                Console.WriteLine($"privateField value: {value}");
            }

            MethodInfo privateMethodInfo = classType.GetMethod("PrivateMethod", BindingFlags.NonPublic | BindingFlags.Instance);
            if (privateMethodInfo != null)
            {
                privateMethodInfo.Invoke(instance, null);
            }

            // Updating a private property using reflection
            PropertyInfo privatePropertyInfo = classType.GetProperty("PrivateProperty", BindingFlags.NonPublic | BindingFlags.Instance);
            if (privatePropertyInfo != null)
            {
                privatePropertyInfo.SetValue(instance, "Updated Value");
                string updatedValue = (string)privatePropertyInfo.GetValue(instance);
                Console.WriteLine($"privateProperty value after update: {updatedValue}");
            }

            // Calling a private method with parameters using reflection
            MethodInfo privateMethodWithParameters = classType.GetMethod("PrivateMethodWithParameter", BindingFlags.NonPublic | BindingFlags.Instance);
            if (privateMethodWithParameters != null)
            {
                // Parameters for the private method
                object[] parameters = new object[] { "Hello from reflection!" };
                privateMethodWithParameters.Invoke(instance, parameters);
            }

            Console.ReadKey();
        }
    }
}
