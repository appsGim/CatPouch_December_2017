using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

/// <summary>
/// Summary description for UtilsExpression
/// </summary>
public static class UtilsExpression
{
    public static Expression ApplyFilter(string opr, Expression left, Expression right)
    {
        Expression InnerLambda = null;
        switch (opr.ToUpper())
        {
            case "Equal":
            case "==":
            case "=":
                InnerLambda = Expression.Equal(left, right);
                break;
            case "Less":
            case "<":
                InnerLambda = Expression.LessThan(left, right);
                break;
            case "Greater":
            case ">":
                InnerLambda = Expression.GreaterThan(left, right);
                break;
            case "GreaterOrEqual":
            case ">=":
                InnerLambda = Expression.GreaterThanOrEqual(left, right);
                break;
            case "LessOrEqual":
            case "<=":
                InnerLambda = Expression.LessThanOrEqual(left, right);
                break;
            case "<>":
            case "!=":
                InnerLambda = Expression.NotEqual(left, right);
                break;
            case "AND":
            case "&&":
                InnerLambda = Expression.And(left, right);
                break;
            case "OR":
            case "||":
                InnerLambda = Expression.Or(left, right);
                break;
            case "CONTAINS":
            case "LIKE":
                InnerLambda = Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), right);
                break;
            case "NOTLIKE":
                InnerLambda = Expression.Not(Expression.Call(left, typeof(string).GetMethod("Contains", new Type[] { typeof(string) }), right));
                break;
        }
        return InnerLambda;
    }


    public static IQueryable Set_IQueryable(string ObjectType, string stringValue,
                                                      Type ElementType, string ColumnName,
                                                      string whereCondition,
                                                      ref IQueryable source)
    {
        object Condition = null;
        switch (ObjectType.ToLower())
        {
            case "int":
            case "int32":
                Condition = int.Parse(stringValue);
                break;
            case "int16":
                Condition = Int16.Parse(stringValue);
                break;
            case "int64":
                Condition = Int64.Parse(stringValue);
                break;
            case "guid":
                Condition = Guid.Parse(stringValue);
                break;
            case "string":
                Condition = stringValue;
                break;
            case "datetime":
                Condition = DateTime.ParseExact(stringValue, "yyyy-MM-dd HH:00:00", null);
                break;
            default: break;
        }

        ConstantExpression Condition_expression = Expression.Constant(Condition);
        ParameterExpression parameter = Expression.Parameter(ElementType);
        MemberExpression property = Expression.Property(parameter, ColumnName);
        if (Nullable.GetUnderlyingType(property.Type) != null)
        {
            property = Expression.Property(property, "Value");
        }

        BinaryExpression comparison = null;

        switch (whereCondition)

        {
            case "Equal":
            case "==":
            case "=":
                comparison = Expression.Equal(property, Condition_expression);

                break;
            case "GreaterOrEqual":
            case ">=":
                comparison = Expression.GreaterThanOrEqual(property, Condition_expression);

                break;
            case "LessOrEqual":
            case "<=":
                comparison = Expression.LessThanOrEqual(property, Condition_expression);

                break;
            case "Greater":
            case ">":
                comparison = Expression.GreaterThan(property, Condition_expression);

                break;
            case "Less":
            case "<":
                comparison = Expression.LessThan(property, Condition_expression);

                break;
        }

        LambdaExpression lambda = Expression.Lambda(comparison, parameter);

        MethodCallExpression where = Expression.Call(
          typeof(Queryable),
          "Where",
          new Type[] { source.ElementType },
          new Expression[] { source.Expression, Expression.Quote(lambda) });

        return source.Provider.CreateQuery(where);
    }


    public static IQueryable Set_IQueryableRange(string ObjectType, string stringValue, string stringValue2,
                                                     Type ElementType, string ColumnName,
                                                     ref IQueryable source)
    {
        object Condition = null, Condition2 = null;
        switch (ObjectType.ToLower())
        {
            case "int":
            case "int32":
                Condition = string.IsNullOrEmpty(stringValue)?int.MinValue :   int.Parse(stringValue);
                Condition2 = string.IsNullOrEmpty(stringValue2) ? int.MaxValue : int.Parse(stringValue2);
                break;
            case "int16":
                Condition = string.IsNullOrEmpty(stringValue) ? Int16.MinValue : Int16.Parse(stringValue);
                Condition2 = string.IsNullOrEmpty(stringValue2) ? Int16.MaxValue : Int16.Parse(stringValue2);
                break;
            case "int64":
                Condition = string.IsNullOrEmpty(stringValue) ? Int64.MinValue : Int64.Parse(stringValue);
                Condition2 = string.IsNullOrEmpty(stringValue2) ? Int64.MaxValue : Int64.Parse(stringValue2);
                break;
            case "double":
                Condition = string.IsNullOrEmpty(stringValue) ? double.MinValue : double.Parse(stringValue);
                Condition2 = string.IsNullOrEmpty(stringValue2) ? double.MaxValue : double.Parse(stringValue2);
                break;
            case "datetime":
                Condition = string.IsNullOrEmpty(stringValue) ? DateTime.MinValue: DateTime.ParseExact(stringValue, "yyyy-MM-dd HH:00:00", null);
                Condition2 = string.IsNullOrEmpty(stringValue2) ? DateTime.MaxValue : DateTime.ParseExact(stringValue2, "yyyy-MM-dd HH:00:00", null);
                break;
            default: break;
        }

        ConstantExpression Condition_expression = Expression.Constant(Condition);
        ConstantExpression Condition_expression2 = Expression.Constant(Condition2);
        ParameterExpression parameter = Expression.Parameter(ElementType);
        ParameterExpression parameter2 = Expression.Parameter(ElementType);
        MemberExpression property = Expression.Property(parameter, ColumnName);
        MemberExpression property2 = Expression.Property(parameter2, ColumnName);
        if (Nullable.GetUnderlyingType(property.Type) != null)
        {
            property = Expression.Property(property, "Value");
        }
        if (Nullable.GetUnderlyingType(property2.Type) != null)
        {
            property2 = Expression.Property(property2, "Value");
        }
        BinaryExpression comparison = null;
        BinaryExpression comparison2 = null;


        comparison = Expression.GreaterThanOrEqual(property, Condition_expression);

        comparison2 = Expression.LessThanOrEqual(property2, Condition_expression2);


        LambdaExpression lambda = Expression.Lambda(comparison, parameter);
        LambdaExpression lambda2 = Expression.Lambda(comparison2, parameter2);


        switch (ObjectType.ToLower().Contains("int"))
        {
            case true:
                      
                break;

            case false:

                break;

        }



        MethodCallExpression where = Expression.Call(
          typeof(Queryable),
          "Where",
          new Type[] { source.ElementType },
          new Expression[] { source.Expression, Expression.Quote(lambda) });

        IQueryable iquery = source.Provider.CreateQuery(where);

        MethodCallExpression where2 = Expression.Call(
       typeof(Queryable),
       "Where",
       new Type[] { iquery.ElementType },
       new Expression[] { iquery.Expression, Expression.Quote(lambda2) });


        

        return iquery.Provider.CreateQuery(where2);

    }
}