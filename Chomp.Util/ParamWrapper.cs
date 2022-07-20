using System;
using System.Collections.Generic;
using SoulsFormats;

namespace Chomp.Util;

internal class ParamWrapper : IComparable<ParamWrapper>
{
	public PARAM Param;

	public PARAMDEF AppliedParamDef;

	public bool Error { get; }

	public string Name { get; }

	public string Description { get; }

	public List<PARAM.Row> Rows => Param.Rows;

	public ParamWrapper(string name, PARAM param, PARAMDEF paramdef)
	{
		Name = name;
		Param = param;
		AppliedParamDef = paramdef;
	}

	public int CompareTo(ParamWrapper other)
	{
		return Name.CompareTo(other.Name);
	}
}
