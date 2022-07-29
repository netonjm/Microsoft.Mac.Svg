namespace Microsoft.Mac.Svg.Operations
{
    public class Operation
    {

    }

    #region Translate

    public class Translate : Operation
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class Translate3D : Operation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    public class TranslateX : Operation
    {
        public float X { get; set; }
    }

    public class TranslateY : Operation
    {
        public float Y { get; set; }
    }

    public class TranslateZ : Operation
    {
        public float Z { get; set; }
    }

    #endregion

    #region Scale

    public class Scale : Operation
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public class ScaleX : Operation
    {
        public float X { get; set; }
    }

    public class ScaleY : Operation
    {
        public float Y { get; set; }
    }

    public class ScaleZ : Operation
    {
        public float Z { get; set; }
    }

    public class Scale3D : Operation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
    }

    #endregion

    #region Rotate

    public class Rotate : Operation
    {
        public float Angle { get; set; }
    }

    public class Rotate3D : Operation
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Angle { get; set; }
    }

    public class RotateX : Operation
    {
        public float Angle { get; set; }
    }

    public class RotateY : Operation
    {
        public float Angle { get; set; }
    }

    public class RotateZ : Operation
    {
        public float Angle { get; set; }
    }

    #endregion

    public static class OperationBuilder
    {
        public static Operation Build(string data)
        {
            var index = data.IndexOf('(');
            if (index == -1)
                throw new System.NotImplementedException("");

            var command = data.Substring(0, index).Trim();
            data = data.Substring(index + 1);
            index = data.IndexOf(')');
            if (index == -1)
                throw new System.NotImplementedException("");

            Operation operation = null;
            var arguments = data.Substring(0, index).Trim();
            if (command == "translate")
            {
                var split = arguments.Split(' ');
                operation = new Translate() { X = float.Parse(split[0]),Y = float.Parse(split[1]) };
            }

            if (operation == null)
                throw new System.NotImplementedException($"{command} not implemented");

            return operation;
        }
    }
}
