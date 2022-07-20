using System;
using System.Collections.Generic;
using System.Text;

namespace Chomp.Util;

public class StringToFormula
{
	private string[] _operators = new string[5] { "-", "+", "/", "*", "^" };

	private Func<double, double, double>[] _operations = new Func<double, double, double>[5]
	{
		(double a1, double a2) => a1 - a2,
		(double a1, double a2) => a1 + a2,
		(double a1, double a2) => a1 / a2,
		(double a1, double a2) => a1 * a2,
		(double a1, double a2) => Math.Pow(a1, a2)
	};

	public double Eval(string expression)
	{
		List<string> tokens = getTokens(expression);
		Stack<double> operandStack = new Stack<double>();
		Stack<string> operatorStack = new Stack<string>();
		int tokenIndex = 0;
		while (tokenIndex < tokens.Count)
		{
			string token = tokens[tokenIndex];
			if (token == "(")
			{
				string subExpr = getSubExpression(tokens, ref tokenIndex);
				operandStack.Push(Eval(subExpr));
				continue;
			}
			if (token == ")")
			{
				throw new ArgumentException("Mis-matched parentheses in expression");
			}
			if (Array.IndexOf(_operators, token) >= 0)
			{
				while (operatorStack.Count > 0 && Array.IndexOf(_operators, token) < Array.IndexOf(_operators, operatorStack.Peek()))
				{
					string op = operatorStack.Pop();
					double arg3 = operandStack.Pop();
					double arg1 = operandStack.Pop();
					operandStack.Push(_operations[Array.IndexOf(_operators, op)](arg1, arg3));
				}
				operatorStack.Push(token);
			}
			else
			{
				operandStack.Push(double.Parse(token));
			}
			tokenIndex++;
		}
		while (operatorStack.Count > 0)
		{
			string op2 = operatorStack.Pop();
			double arg4 = operandStack.Pop();
			double arg2 = operandStack.Pop();
			operandStack.Push(_operations[Array.IndexOf(_operators, op2)](arg2, arg4));
		}
		return operandStack.Pop();
	}

	private string getSubExpression(List<string> tokens, ref int index)
	{
		StringBuilder subExpr = new StringBuilder();
		int parenlevels = 1;
		index++;
		while (index < tokens.Count && parenlevels > 0)
		{
			string token = tokens[index];
			if (tokens[index] == "(")
			{
				parenlevels++;
			}
			if (tokens[index] == ")")
			{
				parenlevels--;
			}
			if (parenlevels > 0)
			{
				subExpr.Append(token);
			}
			index++;
		}
		if (parenlevels > 0)
		{
			throw new ArgumentException("Mis-matched parentheses in expression");
		}
		return subExpr.ToString();
	}

	private List<string> getTokens(string expression)
	{
		string operators = "()^*/+-";
		List<string> tokens = new List<string>();
		StringBuilder sb = new StringBuilder();
		string text = expression.Replace(" ", string.Empty);
		for (int i = 0; i < text.Length; i++)
		{
			char c = text[i];
			if (operators.IndexOf(c) >= 0)
			{
				if (sb.Length > 0)
				{
					tokens.Add(sb.ToString());
					sb.Length = 0;
				}
				tokens.Add(c.ToString());
			}
			else
			{
				sb.Append(c);
			}
		}
		if (sb.Length > 0)
		{
			tokens.Add(sb.ToString());
		}
		return tokens;
	}
}
