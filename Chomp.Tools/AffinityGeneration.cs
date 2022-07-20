using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Chomp.Properties;
using Chomp.Util;
using SoulsFormats;

namespace Chomp.Tools;

internal static class AffinityGeneration
{
	public static void GenerateAffinityRows(DataGridViewRow paramRow, ParamWrapper wrapper, DataGridView dgvRows, GameMode gameMode)
	{
		if (paramRow.Index != 45)
		{
			Utility.ShowError("You can't generate Affinity rows outside of the EquipWeaponParam.");
			return;
		}
		if (dgvRows.SelectedCells.Count == 0)
		{
			Utility.ShowError("You can't generate Affinity rows without a row selected!");
			return;
		}
		string configDir = "Tools\\AffinityGeneration\\\\";
		if (!Directory.Exists(configDir))
		{
			Utility.ShowError("Affinity Generation configuration directory not found.");
			return;
		}
		int index = dgvRows.SelectedCells[0].RowIndex;
		PARAM.Row currentRow = wrapper.Rows[index];
		Console.WriteLine(currentRow.Name);
		FileInfo[] files = new DirectoryInfo(configDir).GetFiles("*.txt");
		FileInfo[] array = files;
		foreach (FileInfo file in array)
		{
			string rawName = file.Name;
			char splitter = "-".ToCharArray()[0];
			string offset = rawName.Split(splitter)[0].Trim();
			string name = rawName.Split(splitter)[1].Trim().Replace(".txt", "");
			List<string[]> instructions = new List<string[]>();
			using (StreamReader reader = new StreamReader(file.FullName))
			{
				while (!reader.EndOfStream)
				{
					string[] values = reader.ReadLine().Split(';');
					instructions.Add(values);
				}
			}
			int id = currentRow.ID + Convert.ToInt32(offset);
			string new_name = currentRow.Name + " [" + name + "]";
			PARAM.Row newRow = new PARAM.Row(id, new_name, wrapper.AppliedParamDef);
			for (int i = 0; i < currentRow.Cells.Count; i++)
			{
				newRow.Cells[i].Value = currentRow.Cells[i].Value;
			}
			foreach (string[] instruction in instructions)
			{
				ModifyAffinityField(currentRow, newRow, instruction);
			}
			wrapper.Rows.Add(newRow);
			wrapper.Rows.Sort((PARAM.Row r1, PARAM.Row r2) => r1.ID.CompareTo(r2.ID));
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Affinity rows generated!", "Affinity Generator");
		}
	}

	public static void ModifyAffinityField(PARAM.Row baseRow, PARAM.Row row, string[] instruction)
	{
		string instruction_field = instruction[0];
		string instruction_type = instruction[1];
		string instruction_value = instruction[2];
		string base_STR_correction = "";
		string base_DEX_correction = "";
		string base_INT_correction = "";
		string base_FTH_correction = "";
		string base_HIGHEST_correction = "";
		string base_PHYSICAL_damage = "";
		string base_MAGIC_damage = "";
		string base_FIRE_damage = "";
		string base_LIGHTNING_damage = "";
		string base_HOLY_damage = "";
		foreach (PARAM.Cell cell2 in baseRow.Cells)
		{
			PARAMDEF.DefType type2 = cell2.Def.DisplayType;
			string name2 = cell2.Def.InternalName.ToString();
			string value2 = cell2.Value.ToString();
			if (type2 != PARAMDEF.DefType.dummy8)
			{
				switch (name2)
				{
				case "correctStrength":
					base_STR_correction = value2;
					break;
				case "correctAgility":
					base_DEX_correction = value2;
					break;
				case "correctMagic":
					base_INT_correction = value2;
					break;
				case "correctFaith":
					base_FTH_correction = value2;
					break;
				case "attackBasePhysics":
					base_PHYSICAL_damage = value2;
					break;
				case "attackBaseMagic":
					base_MAGIC_damage = value2;
					break;
				case "attackBaseFire":
					base_FIRE_damage = value2;
					break;
				case "attackBaseThunder":
					base_LIGHTNING_damage = value2;
					break;
				case "attackBaseDark":
					base_HOLY_damage = value2;
					break;
				case "disableGemAttr":
					cell2.Value = 0;
					break;
				}
			}
		}
		List<string> obj = new List<string> { base_STR_correction, base_DEX_correction, base_INT_correction, base_FTH_correction };
		float highest_value = 0f;
		foreach (string item in obj)
		{
			float value = Convert.ToSingle(item);
			if (value >= highest_value)
			{
				highest_value = value;
			}
		}
		base_HIGHEST_correction = Convert.ToString(highest_value);
		int index = 0;
		foreach (PARAM.Cell cell in row.Cells)
		{
			PARAMDEF.DefType type = cell.Def.DisplayType;
			string name = cell.Def.InternalName;
			cell.Value.ToString();
			string base_value = baseRow.Cells[index].Value.ToString();
			string cell_formula = "";
			StringToFormula stf = new StringToFormula();
			if (type != PARAMDEF.DefType.dummy8 && instruction_field == name)
			{
				switch (instruction_type)
				{
				case "SET":
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						cell.Value = Convert.ToSByte(instruction_value);
						break;
					case PARAMDEF.DefType.u8:
						cell.Value = Convert.ToByte(instruction_value);
						break;
					case PARAMDEF.DefType.s16:
						cell.Value = Convert.ToInt16(instruction_value);
						break;
					case PARAMDEF.DefType.u16:
						cell.Value = Convert.ToUInt16(instruction_value);
						break;
					case PARAMDEF.DefType.s32:
						cell.Value = Convert.ToInt32(instruction_value);
						break;
					case PARAMDEF.DefType.u32:
						cell.Value = Convert.ToUInt32(instruction_value);
						break;
					case PARAMDEF.DefType.f32:
						cell.Value = Convert.ToSingle(instruction_value);
						break;
					case PARAMDEF.DefType.fixstr:
						cell.Value = Convert.ToString(instruction_value);
						break;
					case PARAMDEF.DefType.fixstrW:
						cell.Value = Convert.ToString(instruction_value);
						break;
					}
					break;
				case "CALC":
				{
					cell_formula = instruction_value.Replace("x", base_value);
					double result = stf.Eval(cell_formula);
					result = (float)Math.Floor(result);
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						cell.Value = Convert.ToSByte(result);
						break;
					case PARAMDEF.DefType.u8:
						cell.Value = Convert.ToByte(result);
						break;
					case PARAMDEF.DefType.s16:
						cell.Value = Convert.ToInt16(result);
						break;
					case PARAMDEF.DefType.u16:
						cell.Value = Convert.ToUInt16(result);
						break;
					case PARAMDEF.DefType.s32:
						cell.Value = Convert.ToInt32(result);
						break;
					case PARAMDEF.DefType.u32:
						cell.Value = Convert.ToUInt32(result);
						break;
					case PARAMDEF.DefType.f32:
						cell.Value = Convert.ToSingle(result);
						break;
					}
					break;
				}
				case "STAT_CALC":
				{
					if (instruction_value.Contains("STR"))
					{
						cell_formula = instruction_value.Replace("STR", base_STR_correction);
					}
					else if (instruction_value.Contains("DEX"))
					{
						cell_formula = instruction_value.Replace("DEX", base_DEX_correction);
					}
					else if (instruction_value.Contains("INT"))
					{
						cell_formula = instruction_value.Replace("INT", base_INT_correction);
					}
					else if (instruction_value.Contains("FTH"))
					{
						cell_formula = instruction_value.Replace("FTH", base_FTH_correction);
					}
					else if (instruction_value.Contains("HIGHEST"))
					{
						cell_formula = instruction_value.Replace("HIGHEST", base_HIGHEST_correction);
					}
					double stat_result = stf.Eval(cell_formula);
					stat_result = (float)Math.Floor(stat_result);
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						cell.Value = Convert.ToSByte(stat_result);
						break;
					case PARAMDEF.DefType.u8:
						cell.Value = Convert.ToByte(stat_result);
						break;
					case PARAMDEF.DefType.s16:
						cell.Value = Convert.ToInt16(stat_result);
						break;
					case PARAMDEF.DefType.u16:
						cell.Value = Convert.ToUInt16(stat_result);
						break;
					case PARAMDEF.DefType.s32:
						cell.Value = Convert.ToInt32(stat_result);
						break;
					case PARAMDEF.DefType.u32:
						cell.Value = Convert.ToUInt32(stat_result);
						break;
					case PARAMDEF.DefType.f32:
						cell.Value = Convert.ToSingle(stat_result);
						break;
					}
					break;
				}
				case "DMG_CALC":
				{
					if (instruction_value.Contains("PHYSICAL"))
					{
						cell_formula = instruction_value.Replace("PHYSICAL", base_PHYSICAL_damage);
					}
					else if (instruction_value.Contains("MAGIC"))
					{
						cell_formula = instruction_value.Replace("MAGIC", base_MAGIC_damage);
					}
					else if (instruction_value.Contains("FIRE"))
					{
						cell_formula = instruction_value.Replace("FIRE", base_FIRE_damage);
					}
					else if (instruction_value.Contains("LIGHTNING"))
					{
						cell_formula = instruction_value.Replace("LIGHTNING", base_LIGHTNING_damage);
					}
					else if (instruction_value.Contains("HOLY"))
					{
						cell_formula = instruction_value.Replace("HOLY", base_HOLY_damage);
					}
					double dmg_result = stf.Eval(cell_formula);
					dmg_result = (float)Math.Floor(dmg_result);
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						cell.Value = Convert.ToSByte(dmg_result);
						break;
					case PARAMDEF.DefType.u8:
						cell.Value = Convert.ToByte(dmg_result);
						break;
					case PARAMDEF.DefType.s16:
						cell.Value = Convert.ToInt16(dmg_result);
						break;
					case PARAMDEF.DefType.u16:
						cell.Value = Convert.ToUInt16(dmg_result);
						break;
					case PARAMDEF.DefType.s32:
						cell.Value = Convert.ToInt32(dmg_result);
						break;
					case PARAMDEF.DefType.u32:
						cell.Value = Convert.ToUInt32(dmg_result);
						break;
					case PARAMDEF.DefType.f32:
						cell.Value = Convert.ToSingle(dmg_result);
						break;
					}
					break;
				}
				}
			}
			index++;
		}
	}
}
