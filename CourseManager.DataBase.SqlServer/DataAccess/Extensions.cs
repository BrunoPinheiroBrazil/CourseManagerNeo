﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CourseManager.DataBase.SqlServer.DataAccess
{
  public static class Extensions
  {
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
      return first.Compose(second, Expression.And);
    }

    public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
    {
      return first.Compose(second, Expression.Or);
    }

    public static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> composer)
    {
      return (Expression<T>)((LambdaExpression)first).Compose(second, composer);
    }

    public static LambdaExpression Compose(this LambdaExpression first, LambdaExpression second, Func<Expression, Expression, Expression> composer)
    {
      // build parameter map (from parameters of second to parameters of first)
      var map = first.Parameters.Select((f, i) => new { f, s = second.Parameters[i] }).ToDictionary(p => p.s, p => p.f);

      // replace parameters in the second lambda expression with parameters from the first
      var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

      // apply composition of lambda expression bodies to parameters from the first expression 
      return Expression.Lambda(composer(first.Body, secondBody), first.Parameters);
    }
  }

  public class ParameterRebinder : ExpressionVisitor
  {
    private readonly Dictionary<ParameterExpression, ParameterExpression> _map;

    public ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
    {
      _map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
    }

    public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
    {
      return new ParameterRebinder(map).Visit(exp);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
      if (_map.TryGetValue(node, out var replacement))
      {
        node = replacement;
      }

      return base.VisitParameter(node);
    }
  }
}
