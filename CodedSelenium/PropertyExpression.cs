namespace CodedSelenium
{
    public class PropertyExpression
    {
        public PropertyExpression(string propertyName, string propertyValue)
        {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            PropertyOperator = PropertyExpressionOperator.EqualTo;
        }

        public PropertyExpression(string propertyName, string propertyValue, PropertyExpressionOperator propertyOperator)
            : this(propertyName, propertyValue)
        {
            PropertyOperator = propertyOperator;
        }

        public string PropertyName { get; set; }

        public string PropertyValue { get; set; }

        public PropertyExpressionOperator PropertyOperator { get; set; }

        public object Clone()
        {
            return (object)new PropertyExpression(PropertyName, PropertyValue, PropertyOperator);
        }

        public override string ToString()
        {
            string template = "{0}.{1}({2})";
            return string.Format(template, PropertyName, PropertyOperator, PropertyValue);
        }
    }
}
