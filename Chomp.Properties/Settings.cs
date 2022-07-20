using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Chomp.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("ExampleMod")]
	public string ProjectName
	{
		get
		{
			return (string)this["ProjectName"];
		}
		set
		{
			this["ProjectName"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("200, 200")]
	public Point WindowLocation
	{
		get
		{
			return (Point)this["WindowLocation"];
		}
		set
		{
			this["WindowLocation"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("776, 584")]
	public Size WindowSize
	{
		get
		{
			return (Size)this["WindowSize"];
		}
		set
		{
			this["WindowSize"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool WindowMaximized
	{
		get
		{
			return (bool)this["WindowMaximized"];
		}
		set
		{
			this["WindowMaximized"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("C:\\Program Files (x86)\\Steam\\steamapps\\common\\ELDEN RING\\Game\\regulation.bin")]
	public string RegulationPath
	{
		get
		{
			return (string)this["RegulationPath"];
		}
		set
		{
			this["RegulationPath"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("250")]
	public int SplitterDistance1
	{
		get
		{
			return (int)this["SplitterDistance1"];
		}
		set
		{
			this["SplitterDistance1"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int SelectedParam
	{
		get
		{
			return (int)this["SelectedParam"];
		}
		set
		{
			this["SelectedParam"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int SelectedRow
	{
		get
		{
			return (int)this["SelectedRow"];
		}
		set
		{
			this["SelectedRow"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int SelectedField
	{
		get
		{
			return (int)this["SelectedField"];
		}
		set
		{
			this["SelectedField"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool VerifyRowDeletion
	{
		get
		{
			return (bool)this["VerifyRowDeletion"];
		}
		set
		{
			this["VerifyRowDeletion"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool IncludeHeaderInCSV
	{
		get
		{
			return (bool)this["IncludeHeaderInCSV"];
		}
		set
		{
			this["IncludeHeaderInCSV"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool IncludeRowNameInCSV
	{
		get
		{
			return (bool)this["IncludeRowNameInCSV"];
		}
		set
		{
			this["IncludeRowNameInCSV"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue(";")]
	public string ExportDelimiter
	{
		get
		{
			return (string)this["ExportDelimiter"];
		}
		set
		{
			this["ExportDelimiter"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("249")]
	public int SplitterDistance2
	{
		get
		{
			return (int)this["SplitterDistance2"];
		}
		set
		{
			this["SplitterDistance2"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string DGVIndices
	{
		get
		{
			return (string)this["DGVIndices"];
		}
		set
		{
			this["DGVIndices"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool ShowCommonParamsOnly
	{
		get
		{
			return (bool)this["ShowCommonParamsOnly"];
		}
		set
		{
			this["ShowCommonParamsOnly"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool ChangedCommonParamView
	{
		get
		{
			return (bool)this["ChangedCommonParamView"];
		}
		set
		{
			this["ChangedCommonParamView"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string ParamsToIgnoreDuringSave
	{
		get
		{
			return (string)this["ParamsToIgnoreDuringSave"];
		}
		set
		{
			this["ParamsToIgnoreDuringSave"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("EldenRing")]
	public string GameType
	{
		get
		{
			return (string)this["GameType"];
		}
		set
		{
			this["GameType"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool ShowConfirmationMessages
	{
		get
		{
			return (bool)this["ShowConfirmationMessages"];
		}
		set
		{
			this["ShowConfirmationMessages"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool UseTextEditor
	{
		get
		{
			return (bool)this["UseTextEditor"];
		}
		set
		{
			this["UseTextEditor"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool FieldExporter_RetainFieldText
	{
		get
		{
			return (bool)this["FieldExporter_RetainFieldText"];
		}
		set
		{
			this["FieldExporter_RetainFieldText"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldExporter_FieldMatch
	{
		get
		{
			return (string)this["FieldExporter_FieldMatch"];
		}
		set
		{
			this["FieldExporter_FieldMatch"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldExporter_FieldMinimum
	{
		get
		{
			return (string)this["FieldExporter_FieldMinimum"];
		}
		set
		{
			this["FieldExporter_FieldMinimum"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldExporter_FieldMaximum
	{
		get
		{
			return (string)this["FieldExporter_FieldMaximum"];
		}
		set
		{
			this["FieldExporter_FieldMaximum"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldExporter_FieldExclusion
	{
		get
		{
			return (string)this["FieldExporter_FieldExclusion"];
		}
		set
		{
			this["FieldExporter_FieldExclusion"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldExporter_FieldInclusion
	{
		get
		{
			return (string)this["FieldExporter_FieldInclusion"];
		}
		set
		{
			this["FieldExporter_FieldInclusion"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string TextEditorPath
	{
		get
		{
			return (string)this["TextEditorPath"];
		}
		set
		{
			this["TextEditorPath"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string SecondaryFilePath
	{
		get
		{
			return (string)this["SecondaryFilePath"];
		}
		set
		{
			this["SecondaryFilePath"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool ParamDifferenceMode
	{
		get
		{
			return (bool)this["ParamDifferenceMode"];
		}
		set
		{
			this["ParamDifferenceMode"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_FieldMatch
	{
		get
		{
			return (string)this["FieldAdjuster_FieldMatch"];
		}
		set
		{
			this["FieldAdjuster_FieldMatch"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_RowRange
	{
		get
		{
			return (string)this["FieldAdjuster_RowRange"];
		}
		set
		{
			this["FieldAdjuster_RowRange"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_RowPartialMatch
	{
		get
		{
			return (string)this["FieldAdjuster_RowPartialMatch"];
		}
		set
		{
			this["FieldAdjuster_RowPartialMatch"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_FieldMinimum
	{
		get
		{
			return (string)this["FieldAdjuster_FieldMinimum"];
		}
		set
		{
			this["FieldAdjuster_FieldMinimum"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_FieldMaximum
	{
		get
		{
			return (string)this["FieldAdjuster_FieldMaximum"];
		}
		set
		{
			this["FieldAdjuster_FieldMaximum"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_FieldExclusion
	{
		get
		{
			return (string)this["FieldAdjuster_FieldExclusion"];
		}
		set
		{
			this["FieldAdjuster_FieldExclusion"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_FieldInclusion
	{
		get
		{
			return (string)this["FieldAdjuster_FieldInclusion"];
		}
		set
		{
			this["FieldAdjuster_FieldInclusion"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_Formula
	{
		get
		{
			return (string)this["FieldAdjuster_Formula"];
		}
		set
		{
			this["FieldAdjuster_Formula"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_ValueMin
	{
		get
		{
			return (string)this["FieldAdjuster_ValueMin"];
		}
		set
		{
			this["FieldAdjuster_ValueMin"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string FieldAdjuster_ValueMax
	{
		get
		{
			return (string)this["FieldAdjuster_ValueMax"];
		}
		set
		{
			this["FieldAdjuster_ValueMax"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool FieldAdjuster_RetainFieldText
	{
		get
		{
			return (bool)this["FieldAdjuster_RetainFieldText"];
		}
		set
		{
			this["FieldAdjuster_RetainFieldText"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool Settings_LogParamSize
	{
		get
		{
			return (bool)this["Settings_LogParamSize"];
		}
		set
		{
			this["Settings_LogParamSize"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool SaveWithoutEncryption
	{
		get
		{
			return (bool)this["SaveWithoutEncryption"];
		}
		set
		{
			this["SaveWithoutEncryption"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool ExportUniqueOnly
	{
		get
		{
			return (bool)this["ExportUniqueOnly"];
		}
		set
		{
			this["ExportUniqueOnly"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool CellView_ShowEditorNames
	{
		get
		{
			return (bool)this["CellView_ShowEditorNames"];
		}
		set
		{
			this["CellView_ShowEditorNames"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool CellView_ShowTypes
	{
		get
		{
			return (bool)this["CellView_ShowTypes"];
		}
		set
		{
			this["CellView_ShowTypes"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool ShowFieldDescriptions
	{
		get
		{
			return (bool)this["ShowFieldDescriptions"];
		}
		set
		{
			this["ShowFieldDescriptions"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool VerboseCSVExport
	{
		get
		{
			return (bool)this["VerboseCSVExport"];
		}
		set
		{
			this["VerboseCSVExport"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_Int_R
	{
		get
		{
			return (int)this["FieldColor_Int_R"];
		}
		set
		{
			this["FieldColor_Int_R"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_Int_G
	{
		get
		{
			return (int)this["FieldColor_Int_G"];
		}
		set
		{
			this["FieldColor_Int_G"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_Int_B
	{
		get
		{
			return (int)this["FieldColor_Int_B"];
		}
		set
		{
			this["FieldColor_Int_B"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_Float_R
	{
		get
		{
			return (int)this["FieldColor_Float_R"];
		}
		set
		{
			this["FieldColor_Float_R"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_Float_G
	{
		get
		{
			return (int)this["FieldColor_Float_G"];
		}
		set
		{
			this["FieldColor_Float_G"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_Float_B
	{
		get
		{
			return (int)this["FieldColor_Float_B"];
		}
		set
		{
			this["FieldColor_Float_B"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("230")]
	public int FieldColor_Bool_R
	{
		get
		{
			return (int)this["FieldColor_Bool_R"];
		}
		set
		{
			this["FieldColor_Bool_R"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("230")]
	public int FieldColor_Bool_G
	{
		get
		{
			return (int)this["FieldColor_Bool_G"];
		}
		set
		{
			this["FieldColor_Bool_G"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("230")]
	public int FieldColor_Bool_B
	{
		get
		{
			return (int)this["FieldColor_Bool_B"];
		}
		set
		{
			this["FieldColor_Bool_B"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_String_R
	{
		get
		{
			return (int)this["FieldColor_String_R"];
		}
		set
		{
			this["FieldColor_String_R"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_String_G
	{
		get
		{
			return (int)this["FieldColor_String_G"];
		}
		set
		{
			this["FieldColor_String_G"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("255")]
	public int FieldColor_String_B
	{
		get
		{
			return (int)this["FieldColor_String_B"];
		}
		set
		{
			this["FieldColor_String_B"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool ShowEnums
	{
		get
		{
			return (bool)this["ShowEnums"];
		}
		set
		{
			this["ShowEnums"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool ShowEnumValueInName
	{
		get
		{
			return (bool)this["ShowEnumValueInName"];
		}
		set
		{
			this["ShowEnumValueInName"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("False")]
	public bool EnableFieldValidation
	{
		get
		{
			return (bool)this["EnableFieldValidation"];
		}
		set
		{
			this["EnableFieldValidation"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool EnableFilterBar
	{
		get
		{
			return (bool)this["EnableFilterBar"];
		}
		set
		{
			this["EnableFilterBar"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue(":")]
	public string Filter_CommandDelimiter
	{
		get
		{
			return (string)this["Filter_CommandDelimiter"];
		}
		set
		{
			this["Filter_CommandDelimiter"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("~")]
	public string Filter_SectionDelimiter
	{
		get
		{
			return (string)this["Filter_SectionDelimiter"];
		}
		set
		{
			this["Filter_SectionDelimiter"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool DisplayBooleanEnumAsCheckbox
	{
		get
		{
			return (bool)this["DisplayBooleanEnumAsCheckbox"];
		}
		set
		{
			this["DisplayBooleanEnumAsCheckbox"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("True")]
	public bool DisableEnumForCustomTypes
	{
		get
		{
			return (bool)this["DisableEnumForCustomTypes"];
		}
		set
		{
			this["DisableEnumForCustomTypes"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int NewRow_RepeatCount
	{
		get
		{
			return (int)this["NewRow_RepeatCount"];
		}
		set
		{
			this["NewRow_RepeatCount"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int NewRow_StepValue
	{
		get
		{
			return (int)this["NewRow_StepValue"];
		}
		set
		{
			this["NewRow_StepValue"] = value;
		}
	}
}
