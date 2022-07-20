using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Chomp.Forms;
using Chomp.Properties;
using Chomp.Tools;
using Chomp.Util;
using SoulsFormats;

namespace Chomp;

public class Main : Form
{
	internal class LoadParamsResult
	{
		public bool Encrypted { get; set; }

		public IBinder ParamBND { get; set; }

		public List<ParamWrapper> ParamWrappers { get; set; }
	}

	private static Settings settings = Settings.Default;

	private bool InvalidationMode;

	private string regulationPath;

	private IBinder regulation;

	private IBinder secondary_regulation;

	private bool encrypted;

	private bool secondary_encrypted;

	private BindingSource rowSource;

	private Dictionary<string, (int Row, int Cell)> dgvIndices;

	private string lastFindRowPattern;

	private string lastFindFieldPattern;

	private int PARAM_NAME_COL;

	private int ROW_ID_COL;

	private int ROW_NAME_COL = 1;

	private int FIELD_PARAM_NAME_COL;

	private int FIELD_EDITOR_NAME_COL = 1;

	private int FIELD_VALUE_COL = 2;

	private int FIELD_TYPE_COL = 3;

	private LoadParamsResult primary_result;

	private LoadParamsResult secondary_result;

	private List<PARAMDEF> paramdefs = new List<PARAMDEF>();

	private List<PARAMTDF> paramtdfs = new List<PARAMTDF>();

	private Dictionary<string, PARAMTDF> tdf_dict = new Dictionary<string, PARAMTDF>();

	private List<string> bool_type_tdfs = new List<string>();

	private List<string> custom_type_tdfs = new List<string>();

	private string ATKPARAM_PC = "AtkParam_Pc";

	private string ATKPARAM_NPC = "AtkParam_Npc";

	private string BEHAVIORPARAM_PC = "BehaviorParam_PC";

	private string BEHAVIORPARAM_NPC = "BehaviorParam";

	private IContainer components;

	private DataGridView dgvParams;

	private DataGridView dgvRows;

	private DataGridView dgvCells;

	private OpenFileDialog dataFileDialog;

	private OpenFileDialog importedRegulation;

	private SplitContainer splitContainer1;

	private MenuStrip menuStrip1;

	private ToolStripMenuItem fileToolStripMenuItem;

	private ToolStripMenuItem openToolStripMenuItem;

	private ToolStripMenuItem saveToolStripMenuItem;

	private ToolStripMenuItem restoreToolStripMenuItem;

	private ToolStripMenuItem exploreToolStripMenuItem;

	private ToolStripMenuItem editToolStripMenuItem;

	private ToolStripMenuItem addRowToolStripMenuItem;

	private ToolStripMenuItem deleteRowToolStripMenuItem;

	private DataGridViewTextBoxColumn dgvParamsParamCol;

	private DataGridViewTextBoxColumn dgvRowsIDCol;

	private DataGridViewTextBoxColumn dgvRowsNameCol;

	private SplitContainer splitContainer2;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem findRowToolStripMenuItem;

	private ToolStripMenuItem gotoRowToolStripMenuItem;

	private ToolStripMenuItem findNextRowToolStripMenuItem;

	private ToolStripMenuItem findFieldToolStripMenuItem;

	private ToolStripMenuItem findNextFieldToolStripMenuItem;

	private StatusStrip statusStrip1;

	private ToolStripStatusLabel processLabel;

	private ToolStripMenuItem duplicateRowToolStripMenuItem;

	private ToolStripMenuItem exportToolStripMenuItem;

	private FolderBrowserDialog fbdExport;

	private ToolStripComboBox toolStripComboBoxGame;

	private ToolStripMenuItem WorkflowToolStripMenuItem;

	private ToolStripMenuItem settingsMenuItem;

	private ToolStripMenuItem fieldAdjusterMenuItem;

	private ToolStripMenuItem ProjectFolderMenuItem;

	private ToolStripSeparator toolStripSeparator5;

	private ToolStripMenuItem ToolStripMenuItem;

	private ToolStripMenuItem importDataMenuItem;

	private ToolStripMenuItem exportDataMenuItem;

	private ToolStripSeparator toolStripSeparator7;

	private ToolStripMenuItem massImportDataMenuItem;

	private ToolStripMenuItem massExportDataMenuItem;

	private ToolStripSeparator toolStripSeparator8;

	private ToolStripMenuItem importRowNames_Project_MenuItem;

	private ToolStripMenuItem importRowNames_Stock_MenuItem;

	private ToolStripSeparator toolStripSeparator9;

	private ToolStripMenuItem fieldExporterMenuItem;

	private ToolStripMenuItem valueReferenceFinderMenuItem;

	private ToolStripMenuItem rowReferenceFinderMenuItem;

	private ToolStripMenuItem affinityGeneratorMenuItem;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem exportRowNames_Project_MenuItem;

	private ToolStripMenuItem viewToolStripMenuItem;

	private ToolStripMenuItem viewSettingsToolStripMenuItem;

	private ToolStripMenuItem logParamSizesToolStripMenuItem;

	private ToolStripMenuItem toggleFieldNameTypeToolStripMenuItem;

	private ToolStripMenuItem toggleFieldTypeVisibilityToolStripMenuItem;

	private ContextMenuStrip fieldContextMenu;

	private ToolStripMenuItem GotoReference6MenuItem;

	private DataGridViewTextBoxColumn dgvCellsNameCol;

	private DataGridViewTextBoxColumn dgvCellsEditorNameCol;

	private DataGridViewTextBoxColumn dgvCellsValueCol;

	private DataGridViewTextBoxColumn dgvCellsTypeCol;

	private ToolStripMenuItem GotoReference1MenuItem;

	private ToolStripMenuItem GotoReference2MenuItem;

	private ToolStripMenuItem GotoReference3MenuItem;

	private ToolStripMenuItem GotoReference4MenuItem;

	private ToolStripMenuItem GotoReference5MenuItem;

	private ContextMenuStrip rowContextMenu;

	private ToolStripMenuItem copyToParamMenuItem;

	private ToolStripMenuItem viewInterfaceSettingsToolStripMenuItem;

	private ToolStripMenuItem viewDataSettingsToolStripMenuItem;

	private ToolStripMenuItem secondaryDataToolStripMenuItem;

	private ToolStripMenuItem selectSecondaryFileToolStripMenuItem;

	private OpenFileDialog secondaryFilePath;

	private ToolStripMenuItem showParamDifferencesToolStripMenuItem;

	private ToolStripMenuItem clearSecondaryFileToolMenuItem;

	private MenuStrip menuStrip2;

	private ToolStripTextBox filter_Params;

	private ToolStripMenuItem button_FilterParams;

	private ToolStripPanel BottomToolStripPanel;

	private ToolStripPanel TopToolStripPanel;

	private ToolStripPanel RightToolStripPanel;

	private ToolStripPanel LeftToolStripPanel;

	private ToolStripContentPanel ContentPanel;

	private ToolStripMenuItem button_ResetFilterParams;

	private MenuStrip menuStrip3;

	private ToolStripTextBox filter_Rows;

	private ToolStripMenuItem button_FilterRows;

	private ToolStripMenuItem button_ResetFilterRows;

	private MenuStrip menuStrip4;

	private ToolStripTextBox filter_Cells;

	private ToolStripMenuItem button_FilterCells;

	private ToolStripMenuItem button_ResetFilterCells;

	private ToolTip toolTip_filterParams;

	private ToolTip toolTip_filterRows;

	private ToolTip toolTip_filterCells;

	private ToolStripMenuItem toggleFilterVisibilityToolStripMenuItem;

	private ToolStripMenuItem viewFilterSettingsToolStripMenuItem;

    internal static Settings Settings { get => settings; set => settings = value; }
    public bool InvalidationMode1 { get => InvalidationMode; set => InvalidationMode = value; }
    public string RegulationPath { get => regulationPath; set => regulationPath = value; }
    public IBinder Regulation { get => regulation; set => regulation = value; }
    public IBinder Secondary_regulation { get => secondary_regulation; set => secondary_regulation = value; }
    public bool Encrypted { get => encrypted; set => encrypted = value; }
    public bool Secondary_encrypted { get => secondary_encrypted; set => secondary_encrypted = value; }
    public BindingSource RowSource { get => rowSource; set => rowSource = value; }
    public Dictionary<string, (int Row, int Cell)> DgvIndices { get => dgvIndices; set => dgvIndices = value; }
    public string LastFindRowPattern { get => lastFindRowPattern; set => lastFindRowPattern = value; }
    public string LastFindFieldPattern { get => lastFindFieldPattern; set => lastFindFieldPattern = value; }
    public int PARAM_NAME_COL1 { get => PARAM_NAME_COL; set => PARAM_NAME_COL = value; }
    public int ROW_ID_COL1 { get => ROW_ID_COL; set => ROW_ID_COL = value; }
    public int ROW_NAME_COL1 { get => ROW_NAME_COL; set => ROW_NAME_COL = value; }
    public int FIELD_PARAM_NAME_COL1 { get => FIELD_PARAM_NAME_COL; set => FIELD_PARAM_NAME_COL = value; }
    public int FIELD_EDITOR_NAME_COL1 { get => FIELD_EDITOR_NAME_COL; set => FIELD_EDITOR_NAME_COL = value; }
    public int FIELD_VALUE_COL1 { get => FIELD_VALUE_COL; set => FIELD_VALUE_COL = value; }
    public int FIELD_TYPE_COL1 { get => FIELD_TYPE_COL; set => FIELD_TYPE_COL = value; }
    internal LoadParamsResult Primary_result { get => primary_result; set => primary_result = value; }
    internal LoadParamsResult Secondary_result { get => secondary_result; set => secondary_result = value; }
    public List<PARAMDEF> Paramdefs { get => paramdefs; set => paramdefs = value; }
    public List<PARAMTDF> Paramtdfs { get => paramtdfs; set => paramtdfs = value; }
    public Dictionary<string, PARAMTDF> Tdf_dict { get => tdf_dict; set => tdf_dict = value; }
    public List<string> Bool_type_tdfs { get => bool_type_tdfs; set => bool_type_tdfs = value; }
    public List<string> Custom_type_tdfs { get => custom_type_tdfs; set => custom_type_tdfs = value; }
    public string ATKPARAM_PC1 { get => ATKPARAM_PC; set => ATKPARAM_PC = value; }
    public string ATKPARAM_NPC1 { get => ATKPARAM_NPC; set => ATKPARAM_NPC = value; }
    public string BEHAVIORPARAM_PC1 { get => BEHAVIORPARAM_PC; set => BEHAVIORPARAM_PC = value; }
    public string BEHAVIORPARAM_NPC1 { get => BEHAVIORPARAM_NPC; set => BEHAVIORPARAM_NPC = value; }
    public IContainer Components { get => components; set => components = value; }
    public DataGridView DgvParams { get => dgvParams; set => dgvParams = value; }
    public DataGridView DgvRows { get => dgvRows; set => dgvRows = value; }
    public DataGridView DgvCells { get => dgvCells; set => dgvCells = value; }
    public OpenFileDialog DataFileDialog { get => dataFileDialog; set => dataFileDialog = value; }
    public OpenFileDialog ImportedRegulation { get => importedRegulation; set => importedRegulation = value; }
    public SplitContainer SplitContainer1 { get => splitContainer1; set => splitContainer1 = value; }
    public MenuStrip MenuStrip1 { get => menuStrip1; set => menuStrip1 = value; }
    public ToolStripMenuItem FileToolStripMenuItem { get => fileToolStripMenuItem; set => fileToolStripMenuItem = value; }
    public ToolStripMenuItem OpenToolStripMenuItem { get => openToolStripMenuItem; set => openToolStripMenuItem = value; }
    public ToolStripMenuItem SaveToolStripMenuItem { get => saveToolStripMenuItem; set => saveToolStripMenuItem = value; }
    public ToolStripMenuItem RestoreToolStripMenuItem { get => restoreToolStripMenuItem; set => restoreToolStripMenuItem = value; }
    public ToolStripMenuItem ExploreToolStripMenuItem { get => exploreToolStripMenuItem; set => exploreToolStripMenuItem = value; }
    public ToolStripMenuItem EditToolStripMenuItem { get => editToolStripMenuItem; set => editToolStripMenuItem = value; }
    public ToolStripMenuItem AddRowToolStripMenuItem { get => addRowToolStripMenuItem; set => addRowToolStripMenuItem = value; }
    public ToolStripMenuItem DeleteRowToolStripMenuItem { get => deleteRowToolStripMenuItem; set => deleteRowToolStripMenuItem = value; }
    public DataGridViewTextBoxColumn DgvParamsParamCol { get => dgvParamsParamCol; set => dgvParamsParamCol = value; }
    public DataGridViewTextBoxColumn DgvRowsIDCol { get => dgvRowsIDCol; set => dgvRowsIDCol = value; }
    public DataGridViewTextBoxColumn DgvRowsNameCol { get => dgvRowsNameCol; set => dgvRowsNameCol = value; }
    public SplitContainer SplitContainer2 { get => splitContainer2; set => splitContainer2 = value; }
    public ToolStripSeparator ToolStripSeparator2 { get => toolStripSeparator2; set => toolStripSeparator2 = value; }
    public ToolStripMenuItem FindRowToolStripMenuItem { get => findRowToolStripMenuItem; set => findRowToolStripMenuItem = value; }
    public ToolStripMenuItem GotoRowToolStripMenuItem { get => gotoRowToolStripMenuItem; set => gotoRowToolStripMenuItem = value; }
    public ToolStripMenuItem FindNextRowToolStripMenuItem { get => findNextRowToolStripMenuItem; set => findNextRowToolStripMenuItem = value; }
    public ToolStripMenuItem FindFieldToolStripMenuItem { get => findFieldToolStripMenuItem; set => findFieldToolStripMenuItem = value; }
    public ToolStripMenuItem FindNextFieldToolStripMenuItem { get => findNextFieldToolStripMenuItem; set => findNextFieldToolStripMenuItem = value; }
    public StatusStrip StatusStrip1 { get => statusStrip1; set => statusStrip1 = value; }
    public ToolStripStatusLabel ProcessLabel { get => processLabel; set => processLabel = value; }
    public ToolStripMenuItem DuplicateRowToolStripMenuItem { get => duplicateRowToolStripMenuItem; set => duplicateRowToolStripMenuItem = value; }
    public ToolStripMenuItem ExportToolStripMenuItem { get => exportToolStripMenuItem; set => exportToolStripMenuItem = value; }
    public FolderBrowserDialog FbdExport { get => fbdExport; set => fbdExport = value; }
    public ToolStripComboBox ToolStripComboBoxGame { get => toolStripComboBoxGame; set => toolStripComboBoxGame = value; }
    public ToolStripMenuItem WorkflowToolStripMenuItem1 { get => WorkflowToolStripMenuItem; set => WorkflowToolStripMenuItem = value; }
    public ToolStripMenuItem SettingsMenuItem { get => settingsMenuItem; set => settingsMenuItem = value; }
    public ToolStripMenuItem FieldAdjusterMenuItem { get => fieldAdjusterMenuItem; set => fieldAdjusterMenuItem = value; }
    public ToolStripMenuItem ProjectFolderMenuItem1 { get => ProjectFolderMenuItem; set => ProjectFolderMenuItem = value; }
    public ToolStripSeparator ToolStripSeparator5 { get => toolStripSeparator5; set => toolStripSeparator5 = value; }
    public ToolStripMenuItem ToolStripMenuItem1 { get => ToolStripMenuItem; set => ToolStripMenuItem = value; }
    public ToolStripMenuItem ImportDataMenuItem { get => importDataMenuItem; set => importDataMenuItem = value; }
    public ToolStripMenuItem ExportDataMenuItem { get => exportDataMenuItem; set => exportDataMenuItem = value; }
    public ToolStripSeparator ToolStripSeparator7 { get => toolStripSeparator7; set => toolStripSeparator7 = value; }
    public ToolStripMenuItem MassImportDataMenuItem { get => massImportDataMenuItem; set => massImportDataMenuItem = value; }
    public ToolStripMenuItem MassExportDataMenuItem { get => massExportDataMenuItem; set => massExportDataMenuItem = value; }
    public ToolStripSeparator ToolStripSeparator8 { get => toolStripSeparator8; set => toolStripSeparator8 = value; }
    public ToolStripMenuItem ImportRowNames_Project_MenuItem { get => importRowNames_Project_MenuItem; set => importRowNames_Project_MenuItem = value; }
    public ToolStripMenuItem ImportRowNames_Stock_MenuItem { get => importRowNames_Stock_MenuItem; set => importRowNames_Stock_MenuItem = value; }
    public ToolStripSeparator ToolStripSeparator9 { get => toolStripSeparator9; set => toolStripSeparator9 = value; }
    public ToolStripMenuItem FieldExporterMenuItem { get => fieldExporterMenuItem; set => fieldExporterMenuItem = value; }
    public ToolStripMenuItem ValueReferenceFinderMenuItem { get => valueReferenceFinderMenuItem; set => valueReferenceFinderMenuItem = value; }
    public ToolStripMenuItem RowReferenceFinderMenuItem { get => rowReferenceFinderMenuItem; set => rowReferenceFinderMenuItem = value; }
    public ToolStripMenuItem AffinityGeneratorMenuItem { get => affinityGeneratorMenuItem; set => affinityGeneratorMenuItem = value; }
    public ToolStripSeparator ToolStripSeparator1 { get => toolStripSeparator1; set => toolStripSeparator1 = value; }
    public ToolStripMenuItem ExportRowNames_Project_MenuItem { get => exportRowNames_Project_MenuItem; set => exportRowNames_Project_MenuItem = value; }
    public ToolStripMenuItem ViewToolStripMenuItem { get => viewToolStripMenuItem; set => viewToolStripMenuItem = value; }
    public ToolStripMenuItem ViewSettingsToolStripMenuItem { get => viewSettingsToolStripMenuItem; set => viewSettingsToolStripMenuItem = value; }
    public ToolStripMenuItem LogParamSizesToolStripMenuItem { get => logParamSizesToolStripMenuItem; set => logParamSizesToolStripMenuItem = value; }
    public ToolStripMenuItem ToggleFieldNameTypeToolStripMenuItem { get => toggleFieldNameTypeToolStripMenuItem; set => toggleFieldNameTypeToolStripMenuItem = value; }
    public ToolStripMenuItem ToggleFieldTypeVisibilityToolStripMenuItem { get => toggleFieldTypeVisibilityToolStripMenuItem; set => toggleFieldTypeVisibilityToolStripMenuItem = value; }
    public ContextMenuStrip FieldContextMenu { get => fieldContextMenu; set => fieldContextMenu = value; }
    public ToolStripMenuItem GotoReference6MenuItem1 { get => GotoReference6MenuItem; set => GotoReference6MenuItem = value; }
    public DataGridViewTextBoxColumn DgvCellsNameCol { get => dgvCellsNameCol; set => dgvCellsNameCol = value; }
    public DataGridViewTextBoxColumn DgvCellsEditorNameCol { get => dgvCellsEditorNameCol; set => dgvCellsEditorNameCol = value; }
    public DataGridViewTextBoxColumn DgvCellsValueCol { get => dgvCellsValueCol; set => dgvCellsValueCol = value; }
    public DataGridViewTextBoxColumn DgvCellsTypeCol { get => dgvCellsTypeCol; set => dgvCellsTypeCol = value; }
    public ToolStripMenuItem GotoReference1MenuItem1 { get => GotoReference1MenuItem; set => GotoReference1MenuItem = value; }
    public ToolStripMenuItem GotoReference2MenuItem1 { get => GotoReference2MenuItem; set => GotoReference2MenuItem = value; }
    public ToolStripMenuItem GotoReference3MenuItem1 { get => GotoReference3MenuItem; set => GotoReference3MenuItem = value; }
    public ToolStripMenuItem GotoReference4MenuItem1 { get => GotoReference4MenuItem; set => GotoReference4MenuItem = value; }
    public ToolStripMenuItem GotoReference5MenuItem1 { get => GotoReference5MenuItem; set => GotoReference5MenuItem = value; }
    public ContextMenuStrip RowContextMenu { get => rowContextMenu; set => rowContextMenu = value; }
    public ToolStripMenuItem CopyToParamMenuItem { get => copyToParamMenuItem; set => copyToParamMenuItem = value; }
    public ToolStripMenuItem ViewInterfaceSettingsToolStripMenuItem { get => viewInterfaceSettingsToolStripMenuItem; set => viewInterfaceSettingsToolStripMenuItem = value; }
    public ToolStripMenuItem ViewDataSettingsToolStripMenuItem { get => viewDataSettingsToolStripMenuItem; set => viewDataSettingsToolStripMenuItem = value; }
    public ToolStripMenuItem SecondaryDataToolStripMenuItem { get => secondaryDataToolStripMenuItem; set => secondaryDataToolStripMenuItem = value; }
    public ToolStripMenuItem SelectSecondaryFileToolStripMenuItem { get => selectSecondaryFileToolStripMenuItem; set => selectSecondaryFileToolStripMenuItem = value; }
    public OpenFileDialog SecondaryFilePath { get => secondaryFilePath; set => secondaryFilePath = value; }
    public ToolStripMenuItem ShowParamDifferencesToolStripMenuItem { get => showParamDifferencesToolStripMenuItem; set => showParamDifferencesToolStripMenuItem = value; }
    public ToolStripMenuItem ClearSecondaryFileToolMenuItem { get => clearSecondaryFileToolMenuItem; set => clearSecondaryFileToolMenuItem = value; }
    public MenuStrip MenuStrip2 { get => menuStrip2; set => menuStrip2 = value; }
    public ToolStripTextBox Filter_Params { get => filter_Params; set => filter_Params = value; }
    public ToolStripMenuItem Button_FilterParams { get => button_FilterParams; set => button_FilterParams = value; }
    public ToolStripPanel BottomToolStripPanel1 { get => BottomToolStripPanel; set => BottomToolStripPanel = value; }
    public ToolStripPanel TopToolStripPanel1 { get => TopToolStripPanel; set => TopToolStripPanel = value; }
    public ToolStripPanel RightToolStripPanel1 { get => RightToolStripPanel; set => RightToolStripPanel = value; }
    public ToolStripPanel LeftToolStripPanel1 { get => LeftToolStripPanel; set => LeftToolStripPanel = value; }
    public ToolStripContentPanel ContentPanel1 { get => ContentPanel; set => ContentPanel = value; }
    public ToolStripMenuItem Button_ResetFilterParams { get => button_ResetFilterParams; set => button_ResetFilterParams = value; }
    public MenuStrip MenuStrip3 { get => menuStrip3; set => menuStrip3 = value; }
    public ToolStripTextBox Filter_Rows { get => filter_Rows; set => filter_Rows = value; }
    public ToolStripMenuItem Button_FilterRows { get => button_FilterRows; set => button_FilterRows = value; }
    public ToolStripMenuItem Button_ResetFilterRows { get => button_ResetFilterRows; set => button_ResetFilterRows = value; }
    public MenuStrip MenuStrip4 { get => menuStrip4; set => menuStrip4 = value; }
    public ToolStripTextBox Filter_Cells { get => filter_Cells; set => filter_Cells = value; }
    public ToolStripMenuItem Button_FilterCells { get => button_FilterCells; set => button_FilterCells = value; }
    public ToolStripMenuItem Button_ResetFilterCells { get => button_ResetFilterCells; set => button_ResetFilterCells = value; }
    public ToolTip ToolTip_filterParams { get => toolTip_filterParams; set => toolTip_filterParams = value; }
    public ToolTip ToolTip_filterRows { get => toolTip_filterRows; set => toolTip_filterRows = value; }
    public ToolTip ToolTip_filterCells { get => toolTip_filterCells; set => toolTip_filterCells = value; }
    public ToolStripMenuItem ToggleFilterVisibilityToolStripMenuItem { get => toggleFilterVisibilityToolStripMenuItem; set => toggleFilterVisibilityToolStripMenuItem = value; }
    public ToolStripMenuItem ViewFilterSettingsToolStripMenuItem { get => viewFilterSettingsToolStripMenuItem; set => viewFilterSettingsToolStripMenuItem = value; }

    public Main()
	{
		InitializeComponent();
		regulation = null;
		secondary_regulation = null;
		rowSource = new BindingSource();
		dgvIndices = new Dictionary<string, (int, int)>();
		dgvRows.DataSource = rowSource;
		dgvParams.AutoGenerateColumns = false;
		dgvRows.AutoGenerateColumns = false;
		dgvCells.AutoGenerateColumns = false;
		lastFindRowPattern = "";
		CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
		CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;
	}

	private void Main_Load(object sender, EventArgs e)
	{
		Text = "Yapped - Rune Bear  Rebuild By Tofus Edition";
		toolTip_filterParams.SetToolTip(filter_Params.Control, filter_Params.ToolTipText);
		toolTip_filterRows.SetToolTip(filter_Rows.Control, filter_Rows.ToolTipText);
		toolTip_filterCells.SetToolTip(filter_Cells.Control, filter_Cells.ToolTipText);
		InvalidationMode = false;
		typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, dgvParams, new object[1] { true });
		typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, dgvRows, new object[1] { true });
		typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, dgvCells, new object[1] { true });
		base.Location = settings.WindowLocation;
		if (settings.WindowSize.Width >= MinimumSize.Width && settings.WindowSize.Height >= MinimumSize.Height)
		{
			base.Size = settings.WindowSize;
		}
		if (settings.WindowMaximized)
		{
			base.WindowState = FormWindowState.Maximized;
		}
		toolStripComboBoxGame.ComboBox.DisplayMember = "Name";
		ComboBox.ObjectCollection items = toolStripComboBoxGame.Items;
		object[] modes2 = GameMode.Modes;
		object[] modes = modes2;
		items.AddRange(modes);
		GameMode.GameType game = (GameMode.GameType)Enum.Parse(typeof(GameMode.GameType), settings.GameType);
		toolStripComboBoxGame.SelectedIndex = Array.FindIndex(GameMode.Modes, (GameMode m) => m.Game == game);
		if (toolStripComboBoxGame.SelectedIndex == -1)
		{
			toolStripComboBoxGame.SelectedIndex = 0;
		}
		if (settings.ProjectName == "")
		{
			settings.ProjectName = "ExampleMod";
		}
		regulationPath = settings.RegulationPath;
		splitContainer2.SplitterDistance = settings.SplitterDistance2;
		splitContainer1.SplitterDistance = settings.SplitterDistance1;
		secondaryFilePath.FileName = settings.SecondaryFilePath;
		Settings.Default.ParamDifferenceMode = false;
		BuildParamDefs();
		BuildParamTdfs();
		BuildBoolTypes();
		BuildCustomTypes();
		LoadParams(isSilent: true);
		if (secondaryFilePath.FileName != "")
		{
			LoadSecondaryParams(isSilent: true);
		}
		foreach (Match item in Regex.Matches(settings.DGVIndices, "[^,]+"))
		{
			string[] components = item.Value.Split(':');
			dgvIndices[components[0]] = (int.Parse(components[1]), int.Parse(components[2]));
		}
		if (settings.SelectedParam >= dgvParams.Rows.Count)
		{
			settings.SelectedParam = 0;
		}
		if (dgvParams.Rows.Count > 0)
		{
			dgvParams.ClearSelection();
			dgvParams.Rows[settings.SelectedParam].Selected = true;
			dgvParams.CurrentCell = dgvParams.SelectedCells[0];
		}
	}

	private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
	{
		settings.WindowMaximized = base.WindowState == FormWindowState.Maximized;
		if (base.WindowState == FormWindowState.Normal)
		{
			settings.WindowLocation = base.Location;
			settings.WindowSize = base.Size;
		}
		else
		{
			settings.WindowLocation = base.RestoreBounds.Location;
			settings.WindowSize = base.RestoreBounds.Size;
		}
		settings.GameType = ((GameMode)toolStripComboBoxGame.SelectedItem).Game.ToString();
		settings.RegulationPath = regulationPath;
		settings.SplitterDistance2 = splitContainer2.SplitterDistance;
		settings.SplitterDistance1 = splitContainer1.SplitterDistance;
		if (dgvParams.SelectedCells.Count > 0)
		{
			settings.SelectedParam = dgvParams.SelectedCells[0].RowIndex;
		}
		dgvParams.ClearSelection();
		List<string> components = new List<string>();
		foreach (string key in dgvIndices.Keys)
		{
			components.Add($"{key}:{dgvIndices[key].Row}:{dgvIndices[key].Cell}");
		}
		settings.DGVIndices = string.Join(",", components);
	}

	public string GetProjectDirectory(string subfolder)
	{
		GameMode gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
		return "Projects\\\\" + settings.ProjectName + "\\\\" + subfolder + "\\\\" + gameMode.Directory;
	}

	public string GetParamdexDirectory(string subfolder)
	{
		GameMode gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
		if (subfolder == "")
		{
			return "Paramdex\\\\" + gameMode.Directory;
		}
		return "Paramdex\\\\" + gameMode.Directory + "\\\\" + subfolder;
	}

	private void LoadParams(bool isSilent)
	{
		if (!File.Exists(regulationPath))
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Multiselect = false;
			dialog.Title = "未检索到上次的regulation.bin,请选择regulation.bin文件";
			dialog.Filter = "|*.bin";
			if (dialog.ShowDialog() == DialogResult.OK)
			{
				regulationPath = dialog.FileName;
			}
		}
		primary_result = LoadParamResult(regulationPath, isSecondary: false);
		if (primary_result == null)
		{
			exportToolStripMenuItem.Enabled = false;
			return;
		}
		encrypted = primary_result.Encrypted;
		regulation = primary_result.ParamBND;
		exportToolStripMenuItem.Enabled = encrypted;
		foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
		{
			if (!dgvIndices.ContainsKey(wrapper.Name))
			{
				dgvIndices[wrapper.Name] = (0, 0);
			}
		}
		dgvParams.DataSource = primary_result.ParamWrappers;
		foreach (DataGridViewRow row in (IEnumerable)dgvParams.Rows)
		{
			if (((ParamWrapper)row.DataBoundItem).Error)
			{
				row.Cells[0].Style.BackColor = Color.Pink;
			}
		}
		if (!isSilent && !Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Primary file loaded.", "File Data", MessageBoxButtons.OK);
		}
	}

	private void LoadSecondaryParams(bool isSilent)
	{
		secondary_result = LoadParamResult(Settings.Default.SecondaryFilePath, isSecondary: true);
		if (secondary_result == null)
		{
			Utility.ShowError("Failed to load secondary data file.");
			return;
		}
		secondary_encrypted = secondary_result.Encrypted;
		secondary_regulation = secondary_result.ParamBND;
		if (!isSilent && !Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Secondary file loaded.", "File Data", MessageBoxButtons.OK);
		}
	}

	private void BuildParamDefs()
	{
		paramdefs.Clear();
		string[] files = Directory.GetFiles(GetParamdexDirectory("Defs"), "*.xml");
		string[] array = files;
		foreach (string path in array)
		{
			string paramID = Path.GetFileNameWithoutExtension(path);
			try
			{
				PARAMDEF paramdef = PARAMDEF.XmlDeserialize(path);
				paramdefs.Add(paramdef);
			}
			catch (Exception ex)
			{
				Utility.ShowError($"Failed to load layout {paramID}.txt\r\n\r\n{ex}");
			}
		}
	}

	private void BuildParamTdfs()
	{
		paramtdfs.Clear();
		tdf_dict.Clear();
		string[] files = Directory.GetFiles(GetParamdexDirectory("Tdfs"), "*.tdf");
		string[] array = files;
		foreach (string path in array)
		{
			string tdfID = Path.GetFileNameWithoutExtension(path);
			string tdfText = File.ReadAllText(path);
			try
			{
				PARAMTDF paramtdf = new PARAMTDF(tdfText);
				paramtdfs.Add(paramtdf);
			}
			catch (Exception ex)
			{
				Utility.ShowError($"Failed to load layout {tdfID}.txt\r\n\r\n{ex}");
			}
		}
		foreach (PARAMTDF paramtdf2 in paramtdfs)
		{
			string tdf_name = paramtdf2.Name;
			try
			{
				tdf_dict.Add(tdf_name, paramtdf2);
			}
			catch (Exception ex2)
			{
				Utility.ShowError($"Failed to add TDF {tdf_name}.\r\n\r\n{ex2}");
			}
		}
	}

	private void BuildBoolTypes()
	{
		bool_type_tdfs.Clear();
		string boolean_type_file = GetParamdexDirectory("Meta") + "\\bool_types.txt";
		if (!File.Exists(boolean_type_file))
		{
			bool_type_tdfs = null;
			return;
		}
		StreamReader reader = null;
		try
		{
			reader = new StreamReader(File.OpenRead(boolean_type_file));
		}
		catch (Exception ex)
		{
			Utility.ShowError($"Failed to open {boolean_type_file}.\r\n\r\n{ex}");
			return;
		}
		while (!reader.EndOfStream)
		{
			string line = reader.ReadLine();
			bool_type_tdfs.Add(line);
		}
	}

	private void BuildCustomTypes()
	{
		custom_type_tdfs.Clear();
		string custom_type_file = GetParamdexDirectory("Meta") + "\\customizable_types.txt";
		if (!File.Exists(custom_type_file))
		{
			custom_type_tdfs = null;
			return;
		}
		StreamReader reader = null;
		try
		{
			reader = new StreamReader(File.OpenRead(custom_type_file));
		}
		catch (Exception ex)
		{
			Utility.ShowError($"Failed to open {custom_type_file}.\r\n\r\n{ex}");
			return;
		}
		while (!reader.EndOfStream)
		{
			string line = reader.ReadLine();
			custom_type_tdfs.Add(line);
		}
	}

	private LoadParamsResult LoadParamResult(string target_path, bool isSecondary)
	{
		if (!File.Exists(target_path))
		{
			Utility.ShowError("Parambnd not found:\r\n" + target_path + "\r\nPlease browse to the Data0.bdt or parambnd you would like to edit.");
			return null;
		}
		LoadParamsResult result = new LoadParamsResult();
		GameMode gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
		try
		{
			if (SoulsFile<BND4>.Is(target_path))
			{
				result.ParamBND = SoulsFile<BND4>.Read(target_path);
				result.Encrypted = false;
			}
			else if (SoulsFile<BND3>.Is(target_path))
			{
				result.ParamBND = SoulsFile<BND3>.Read(target_path);
				result.Encrypted = false;
			}
			else if (gameMode.Game == GameMode.GameType.DarkSouls2 || gameMode.Game == GameMode.GameType.DarkSouls2Scholar)
			{
				result.ParamBND = Utility.DecryptDS2Regulation(target_path);
				result.Encrypted = true;
			}
			else if (gameMode.Game == GameMode.GameType.DarkSouls3)
			{
				result.ParamBND = SFUtil.DecryptDS3Regulation(target_path);
				result.Encrypted = true;
			}
			else
			{
				if (gameMode.Game != GameMode.GameType.EldenRing)
				{
					throw new FormatException("Unrecognized file format.");
				}
				result.ParamBND = SFUtil.DecryptERRegulation(target_path);
				result.Encrypted = true;
			}
		}
		catch (DllNotFoundException ex3) when (ex3.Message.Contains("oo2core_6_win64.dll"))
		{
			Utility.ShowError("In order to load Sekiro params, you must copy oo2core_6_win64.dll from Sekiro into Yapped's lib folder.");
			return null;
		}
		catch (Exception ex2)
		{
			Utility.ShowError($"Failed to load parambnd:\r\n{target_path}\r\n\r\n{ex2}");
			return null;
		}
		if (!isSecondary)
		{
			processLabel.Text = target_path;
		}
		result.ParamWrappers = new List<ParamWrapper>();
		foreach (BinderFile file in result.ParamBND.Files.Where((BinderFile f) => f.Name.EndsWith(".param")))
		{
			string name = Path.GetFileNameWithoutExtension(file.Name);
			try
			{
				PARAM param = SoulsFile<PARAM>.Read(file.Bytes);
				if (param.ApplyParamdefCarefully(paramdefs))
				{
					ParamWrapper wrapper = new ParamWrapper(name, param, param.AppliedParamdef);
					result.ParamWrappers.Add(wrapper);
				}
			}
			catch (Exception ex)
			{
				Utility.ShowError($"Failed to load param file: {name}.param\r\n\r\n{ex}");
			}
		}
		result.ParamWrappers.Sort();
		return result;
	}

	private void toggleFieldNameTypeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			if (!settings.CellView_ShowEditorNames)
			{
				settings.CellView_ShowEditorNames = true;
			}
			else
			{
				settings.CellView_ShowEditorNames = false;
			}
		}
	}

	private void toggleFieldTypeVisibilityToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			if (!settings.CellView_ShowTypes)
			{
				settings.CellView_ShowTypes = true;
			}
			else
			{
				settings.CellView_ShowTypes = false;
			}
		}
	}

	private void viewSettingsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			if (new SettingsMenu().ShowDialog() == DialogResult.OK && !Settings.Default.ShowConfirmationMessages)
			{
				MessageBox.Show("Settings changed.", "Settings", MessageBoxButtons.OK);
			}
			GenerateProjectDirectories(Settings.Default.ProjectName);
		}
	}

	private void logParamSizesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		string param_size_path = GetProjectDirectory("Logs") + "\\ParamSizes.log";
		if (primary_result != null)
		{
			StreamWriter output_file = new StreamWriter(param_size_path);
			foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
			{
				output_file.WriteLine(wrapper.Name.ToString());
				output_file.WriteLine(wrapper.Param.DetectedSize.ToString());
			}
			output_file.Close();
		}
		if (Settings.Default.UseTextEditor && Settings.Default.TextEditorPath != "")
		{
			try
			{
				Process.Start("\"" + Settings.Default.TextEditorPath + "\"", "\"" + Application.StartupPath + "\\" + param_size_path + "\"");
			}
			catch
			{
				SystemSounds.Hand.Play();
			}
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Param sizes logged to " + param_size_path, "Field Exporter", MessageBoxButtons.OK);
		}
	}

	private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			dataFileDialog.FileName = regulationPath;
			if (dataFileDialog.ShowDialog() == DialogResult.OK)
			{
				regulationPath = dataFileDialog.FileName;
				BuildParamDefs();
				BuildParamTdfs();
				BuildBoolTypes();
				BuildCustomTypes();
				LoadParams(isSilent: false);
			}
		}
	}

	private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			SaveParams(".bak");
			if (!Settings.Default.ShowConfirmationMessages)
			{
				MessageBox.Show("Params saved to " + regulationPath, "Save", MessageBoxButtons.OK);
			}
		}
	}

	private void SaveParams(string backup_format)
	{
		foreach (BinderFile file in regulation.Files)
		{
			foreach (DataGridViewRow item in (IEnumerable)dgvParams.Rows)
			{
				ParamWrapper paramFile = (ParamWrapper)item.DataBoundItem;
				if (Path.GetFileNameWithoutExtension(file.Name) == paramFile.Name)
				{
					try
					{
						file.Bytes = paramFile.Param.Write();
					}
					catch
					{
						MessageBox.Show($"Invalid data, failed to save {paramFile}. Data must be fixed before saving can complete.", "Save", MessageBoxButtons.OK);
						return;
					}
				}
			}
		}
		GameMode gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
		if (!File.Exists(regulationPath + backup_format))
		{
			File.Copy(regulationPath, regulationPath + backup_format);
		}
		if (encrypted && !Settings.Default.SaveWithoutEncryption)
		{
			if (gameMode.Game == GameMode.GameType.DarkSouls2)
			{
				Utility.EncryptDS2Regulation(regulationPath, regulation as BND4);
			}
			else if (gameMode.Game == GameMode.GameType.DarkSouls3)
			{
				SFUtil.EncryptDS3Regulation(regulationPath, regulation as BND4);
			}
			else if (gameMode.Game == GameMode.GameType.EldenRing)
			{
				SFUtil.EncryptERRegulation(regulationPath, regulation as BND4);
			}
			else
			{
				Utility.ShowError("Encryption is not valid for this file.");
			}
		}
		else if (regulation is BND3 bnd3)
		{
			bnd3.Write(regulationPath);
		}
		else if (regulation is BND4 bnd4)
		{
			bnd4.Write(regulationPath);
		}
		SystemSounds.Asterisk.Play();
	}

	private void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		if (File.Exists(regulationPath + ".bak"))
		{
			if (MessageBox.Show("Are you sure you want to restore the backup?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				try
				{
					File.Copy(regulationPath + ".bak", regulationPath, overwrite: true);
					BuildParamDefs();
					BuildParamTdfs();
					BuildBoolTypes();
					BuildCustomTypes();
					LoadParams(isSilent: false);
					SystemSounds.Asterisk.Play();
				}
				catch (Exception ex)
				{
					Utility.ShowError($"Failed to restore backup\r\n\r\n{regulationPath}.bak\r\n\r\n{ex}");
				}
			}
		}
		else
		{
			Utility.ShowError("There is no backup to restore at:\r\n\r\n" + regulationPath + ".bak");
		}
	}

	private void ExploreToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start(Path.GetDirectoryName(regulationPath));
		}
		catch
		{
			SystemSounds.Hand.Play();
		}
	}

	private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		GameMode gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
		_ = regulation;
		string dir = fbdExport.SelectedPath;
		fbdExport.SelectedPath = Path.GetDirectoryName(regulationPath);
		if (fbdExport.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		BND4 paramBND = new BND4
		{
			BigEndian = false,
			Compression = DCX.Type.DCX_DFLT_10000_44_9,
			Extended = 4,
			Unk04 = false,
			Unk05 = false,
			Format = (SoulsFormats.Binder.Format.Names1 | SoulsFormats.Binder.Format.LongOffsets | SoulsFormats.Binder.Format.Compression | SoulsFormats.Binder.Format.Flag6),
			Unicode = true,
			Files = regulation.Files.Where((BinderFile f) => f.Name.EndsWith(".param")).ToList()
		};
		try
		{
			paramBND.Write(dir + "\\gameparam.parambnd.dcx");
		}
		catch (Exception ex2)
		{
			Utility.ShowError($"Failed to write exported parambnds.\r\n\r\n{ex2}");
		}
		if (gameMode.Game != GameMode.GameType.DarkSouls3)
		{
			return;
		}
		BND4 stayBND = new BND4
		{
			BigEndian = false,
			Compression = DCX.Type.DCX_DFLT_10000_44_9,
			Extended = 4,
			Unk04 = false,
			Unk05 = false,
			Format = (SoulsFormats.Binder.Format.Names1 | SoulsFormats.Binder.Format.LongOffsets | SoulsFormats.Binder.Format.Compression | SoulsFormats.Binder.Format.Flag6),
			Unicode = true,
			Files = regulation.Files.Where((BinderFile f) => f.Name.EndsWith(".stayparam")).ToList()
		};
		try
		{
			stayBND.Write(dir + "\\stayparam.parambnd.dcx");
		}
		catch (Exception ex)
		{
			Utility.ShowError($"Failed to write exported parambnds.\r\n\r\n{ex}");
		}
	}

	private void ProjectFolderMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start("Projects");
		}
		catch
		{
			SystemSounds.Hand.Play();
		}
	}

	private void AddRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			CreateRow("Add a new row...");
		}
	}

	private void DuplicateRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		if (dgvRows.SelectedCells.Count == 0)
		{
			Utility.ShowError("You can't duplicate a row without one selected!");
			return;
		}
		int index = dgvRows.SelectedCells[0].RowIndex;
		PARAM.Row oldRow = ((ParamWrapper)rowSource.DataSource).Rows[index];
		if (rowSource.DataSource == null)
		{
			Utility.ShowError("You can't create a row with no param selected!");
			return;
		}
		NewRow newRowForm = new NewRow("Duplicate a row...");
		if (newRowForm.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		int base_id = newRowForm.ResultID;
		string name = newRowForm.ResultName;
		int repeat_count = Settings.Default.NewRow_RepeatCount;
		int step_value = Settings.Default.NewRow_StepValue;
		if (repeat_count == 0)
		{
			repeat_count = 1;
		}
		if (step_value == 0)
		{
			step_value = 1;
		}
		int current_id = base_id;
		PARAM.Row newRow;
		for (int j = 0; j < repeat_count; j++)
		{
			ParamWrapper paramWrapper = (ParamWrapper)rowSource.DataSource;
			if (paramWrapper.Rows.Any((PARAM.Row row) => row.ID == current_id))
			{
				Utility.ShowError($"A row with this ID already exists: {current_id}");
			}
			else
			{
				newRow = new PARAM.Row(current_id, name, paramWrapper.AppliedParamDef);
				rowSource.Add(newRow);
				paramWrapper.Rows.Sort((PARAM.Row r1, PARAM.Row r2) => r1.ID.CompareTo(r2.ID));
				int row_index = paramWrapper.Rows.FindIndex((PARAM.Row row) => row == newRow);
				int displayedRows = dgvRows.DisplayedRowCount(includePartialRow: false);
				dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, row_index - displayedRows / 2);
				dgvRows.ClearSelection();
				dgvRows.Rows[row_index].Selected = true;
				dgvRows.Refresh();
				for (int i = 0; i < oldRow.Cells.Count; i++)
				{
					newRow.Cells[i].Value = oldRow.Cells[i].Value;
				}
			}
			current_id += step_value;
		}
	}

	private PARAM.Row CreateRow(string prompt)
	{
		if (rowSource.DataSource == null)
		{
			Utility.ShowError("You can't create a row with no param selected!");
			return null;
		}
		PARAM.Row row_result = null;
		NewRow newRowForm = new NewRow(prompt);
		if (newRowForm.ShowDialog() == DialogResult.OK)
		{
			int id = newRowForm.ResultID;
			string name = newRowForm.ResultName;
			ParamWrapper paramWrapper = (ParamWrapper)rowSource.DataSource;
			if (paramWrapper.Rows.Any((PARAM.Row row) => row.ID == id))
			{
				Utility.ShowError($"A row with this ID already exists: {id}");
			}
			else
			{
				row_result = new PARAM.Row(id, name, paramWrapper.AppliedParamDef);
				rowSource.Add(row_result);
				paramWrapper.Rows.Sort((PARAM.Row r1, PARAM.Row r2) => r1.ID.CompareTo(r2.ID));
				int index = paramWrapper.Rows.FindIndex((PARAM.Row row) => row == row_result);
				int displayedRows = dgvRows.DisplayedRowCount(includePartialRow: false);
				dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
				dgvRows.ClearSelection();
				dgvRows.Rows[index].Selected = true;
				dgvRows.Refresh();
			}
		}
		return row_result;
	}

	private void DeleteRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode || dgvRows.SelectedCells.Count <= 0)
		{
			return;
		}
		DialogResult choice = DialogResult.Yes;
		if (settings.VerifyRowDeletion)
		{
			choice = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
		}
		if (choice != DialogResult.Yes)
		{
			return;
		}
		int rowIndex = dgvRows.SelectedCells[0].RowIndex;
		rowSource.RemoveAt(rowIndex);
		if (rowIndex == dgvRows.RowCount)
		{
			if (dgvRows.RowCount > 0)
			{
				dgvRows.Rows[dgvRows.RowCount - 1].Selected = true;
			}
			else
			{
				dgvCells.DataSource = null;
			}
		}
	}

	private void FindRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			FindRow findForm = new FindRow("Find row with name...");
			if (findForm.ShowDialog() == DialogResult.OK)
			{
				FindRow(findForm.ResultPattern);
			}
		}
	}

	private void FindNextRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			FindRow(lastFindRowPattern);
		}
	}

	private void FindRow(string pattern)
	{
		if (InvalidationMode)
		{
			return;
		}
		if (rowSource.DataSource == null)
		{
			Utility.ShowError("You can't search for a row when there are no rows!");
			return;
		}
		int startIndex = ((dgvRows.SelectedCells.Count > 0) ? (dgvRows.SelectedCells[0].RowIndex + 1) : 0);
		List<PARAM.Row> rows = ((ParamWrapper)rowSource.DataSource).Rows;
		int index = -1;
		for (int i = 0; i < rows.Count; i++)
		{
			if ((rows[(startIndex + i) % rows.Count].Name ?? "").ToLower().Contains(pattern.ToLower()))
			{
				index = (startIndex + i) % rows.Count;
				break;
			}
		}
		if (index != -1)
		{
			int displayedRows = dgvRows.DisplayedRowCount(includePartialRow: false);
			dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
			dgvRows.ClearSelection();
			dgvRows.Rows[index].Selected = true;
			lastFindRowPattern = pattern;
		}
		else
		{
			Utility.ShowError("No row found matching: " + pattern);
		}
	}

	private void GotoRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		GoToRow gotoForm = new GoToRow();
		if (gotoForm.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		if (rowSource.DataSource == null)
		{
			Utility.ShowError("You can't goto a row when there are no rows!");
			return;
		}
		long id = gotoForm.ResultID;
		int index = ((ParamWrapper)rowSource.DataSource).Rows.FindIndex((PARAM.Row row) => row.ID == id);
		if (index != -1)
		{
			int displayedRows = dgvRows.DisplayedRowCount(includePartialRow: false);
			dgvRows.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
			dgvRows.ClearSelection();
			dgvRows.Rows[index].Selected = true;
		}
		else
		{
			Utility.ShowError($"Row ID not found: {id}");
		}
	}

	private void FindFieldToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			FindField findForm = new FindField("Find field with name...");
			if (findForm.ShowDialog() == DialogResult.OK)
			{
				FindField(findForm.ResultPattern);
			}
		}
	}

	private void FindNextFieldToolStripMenuItem_Click(object sender, EventArgs e)
	{
		FindField(lastFindFieldPattern);
	}

	private void FindField(string pattern)
	{
		if (InvalidationMode)
		{
			return;
		}
		if (dgvCells.DataSource == null)
		{
			Utility.ShowError("You can't search for a field when there are no fields!");
			return;
		}
		int startIndex = ((dgvCells.SelectedCells.Count > 0) ? (dgvCells.SelectedCells[0].RowIndex + 1) : 0);
		PARAM.Cell[] cells = (PARAM.Cell[])dgvCells.DataSource;
		int index = -1;
		for (int i = 0; i < cells.Length; i++)
		{
			if (Settings.Default.CellView_ShowEditorNames)
			{
				if ((cells[(startIndex + i) % cells.Length].Def.DisplayName.ToString() ?? "").ToLower().Contains(pattern.ToLower()))
				{
					index = (startIndex + i) % cells.Length;
					break;
				}
			}
			else if ((cells[(startIndex + i) % cells.Length].Def.InternalName.ToString() ?? "").ToLower().Contains(pattern.ToLower()))
			{
				index = (startIndex + i) % cells.Length;
				break;
			}
		}
		if (index != -1)
		{
			int displayedRows = dgvCells.DisplayedRowCount(includePartialRow: false);
			dgvCells.FirstDisplayedScrollingRowIndex = Math.Max(0, index - displayedRows / 2);
			dgvCells.ClearSelection();
			dgvCells.Rows[index].Selected = true;
			lastFindFieldPattern = pattern;
		}
		else
		{
			Utility.ShowError("No field found matching: " + pattern);
		}
	}

	private void importRowNames_Project_MenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		bool replace = MessageBox.Show("If a row already has a name, would you like to skip it?\r\nClick Yes to skip existing names.\r\nClick No to replace existing names.", "Importing Names", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
		string name_dir = GetProjectDirectory("Names");
		foreach (ParamWrapper paramFile in (List<ParamWrapper>)dgvParams.DataSource)
		{
			if (!File.Exists(name_dir + "\\" + paramFile.Name + ".txt"))
			{
				continue;
			}
			Dictionary<long, string> names = new Dictionary<long, string>();
			string[] array = Regex.Split(File.ReadAllText(name_dir + "\\" + paramFile.Name + ".txt"), "\\s*[\\r\\n]+\\s*");
			string[] array2 = array;
			foreach (string line in array2)
			{
				if (line.Length > 0)
				{
					Match match = Regex.Match(line, "^(\\d+) (.+)$");
					long id = long.Parse(match.Groups[1].Value);
					string text = (names[id] = match.Groups[2].Value);
					string name = text;
				}
			}
			foreach (PARAM.Row row in paramFile.Param.Rows)
			{
				if (names.ContainsKey(row.ID) && (replace || row.Name == null || row.Name == ""))
				{
					row.Name = names[row.ID];
				}
			}
		}
		dgvRows.Refresh();
	}

	private void importRowNames_Stock_MenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		bool replace = MessageBox.Show("If a row already has a name, would you like to skip it?\r\nClick Yes to skip existing names.\r\nClick No to replace existing names.", "Importing Names", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
		string name_dir = GetParamdexDirectory("Names");
		foreach (ParamWrapper paramFile in (List<ParamWrapper>)dgvParams.DataSource)
		{
			if (!File.Exists(name_dir + "\\" + paramFile.Name + ".txt"))
			{
				continue;
			}
			Dictionary<long, string> names = new Dictionary<long, string>();
			string[] array = Regex.Split(File.ReadAllText(name_dir + "\\" + paramFile.Name + ".txt"), "\\s*[\\r\\n]+\\s*");
			string[] array2 = array;
			foreach (string line in array2)
			{
				if (line.Length > 0)
				{
					Match match = Regex.Match(line, "^(\\d+) (.+)$");
					long id = long.Parse(match.Groups[1].Value);
					string text = (names[id] = match.Groups[2].Value);
					string name = text;
				}
			}
			foreach (PARAM.Row row in paramFile.Param.Rows)
			{
				if (names.ContainsKey(row.ID) && (replace || row.Name == null || row.Name == ""))
				{
					row.Name = names[row.ID];
				}
			}
		}
		dgvRows.Refresh();
	}

	private void exportRowNames_Project_MenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		string name_dir = GetProjectDirectory("Names");
		foreach (ParamWrapper paramFile in (List<ParamWrapper>)dgvParams.DataSource)
		{
			StringBuilder sb = new StringBuilder();
			foreach (PARAM.Row row in paramFile.Param.Rows)
			{
				string name = (row.Name ?? "").Trim();
				if (name != "")
				{
					sb.AppendLine($"{row.ID} {name}");
				}
			}
			try
			{
				File.WriteAllText(name_dir + "\\" + paramFile.Name + ".txt", sb.ToString());
			}
			catch (Exception ex)
			{
				Utility.ShowError($"Failed to write name file: {paramFile.Name}.txt\r\n\r\n{ex}");
				break;
			}
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Names exported!", "Export Names", MessageBoxButtons.OK);
		}
	}

	private void importDataMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			ParamWrapper wrapper = (ParamWrapper)dgvParams.CurrentRow.DataBoundItem;
			ImportParamData(wrapper, isSilent: false);
		}
	}

	private void exportDataMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			ParamWrapper wrapper = (ParamWrapper)dgvParams.CurrentRow.DataBoundItem;
			ExportParamData(wrapper, isSilent: false);
		}
	}

	private void fieldExporterMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		FieldExporter fieldExporter = new FieldExporter();
		string log_dir = GetProjectDirectory("Logs");
		if (fieldExporter.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		ParamWrapper paramWrapper = (ParamWrapper)rowSource.DataSource;
		string field_data_path = log_dir + "\\\\FieldValue_" + paramWrapper.Name + ".log";
		char delimiter = settings.ExportDelimiter.ToCharArray()[0];
		string field_match = Settings.Default.FieldExporter_FieldMatch;
		string field_minimum = Settings.Default.FieldExporter_FieldMinimum;
		string field_maximum = Settings.Default.FieldExporter_FieldMaximum;
		string field_exclusions = Settings.Default.FieldExporter_FieldExclusion;
		string field_inclusions = Settings.Default.FieldExporter_FieldInclusion;
		if (field_match == "")
		{
			MessageBox.Show("You did not specify any field names.", "Field Exporter", MessageBoxButtons.OK);
			return;
		}
		if (File.Exists(field_data_path))
		{
			if (MessageBox.Show(field_data_path + " exists. Overwrite?", "Export Field Values", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				return;
			}
		}
		else if (!File.Exists(field_data_path))
		{
			using FileStream fs = File.Create(field_data_path);
			for (byte i = 0; i < 100; i = (byte)(i + 1))
			{
				fs.WriteByte(i);
			}
		}
		StreamWriter output_file = new StreamWriter(field_data_path);
		string header_line = "";
		if (settings.IncludeHeaderInCSV)
		{
			header_line = "Row ID" + settings.ExportDelimiter + "Row Name" + settings.ExportDelimiter + field_match;
			output_file.WriteLine(header_line);
		}
		List<string> unique_list = new List<string>();
		foreach (PARAM.Row row in paramWrapper.Rows)
		{
			string row_line = row.ID + settings.ExportDelimiter;
			if (settings.IncludeRowNameInCSV)
			{
				string row_name = row.Name;
				row_line = row_line + row_name + settings.ExportDelimiter;
			}
			bool isValidRow = false;
			foreach (PARAM.Cell cell in row.Cells)
			{
				PARAMDEF.DefType type = cell.Def.DisplayType;
				string display_name = cell.Def.DisplayName;
				string internal_name = cell.Def.InternalName;
				string value = cell.Value.ToString();
				bool isMatchedField = false;
				if (type == PARAMDEF.DefType.dummy8 || ((!Settings.Default.CellView_ShowEditorNames || !(field_match == display_name)) && (Settings.Default.CellView_ShowEditorNames || !(field_match == internal_name))))
				{
					continue;
				}
				isMatchedField = true;
				if (field_minimum != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						if (Convert.ToSByte(value) < Convert.ToSByte(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u8:
						if (Convert.ToByte(value) < Convert.ToByte(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s16:
						if (Convert.ToInt16(value) < Convert.ToInt16(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u16:
						if (Convert.ToUInt16(value) < Convert.ToUInt16(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s32:
						if (Convert.ToInt32(value) < Convert.ToInt32(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u32:
						if (Convert.ToUInt32(value) < Convert.ToUInt32(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.f32:
						if (Convert.ToSingle(value) < Convert.ToSingle(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					}
				}
				if (field_maximum != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						if (Convert.ToSByte(value) > Convert.ToSByte(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u8:
						if (Convert.ToByte(value) > Convert.ToByte(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s16:
						if (Convert.ToInt16(value) > Convert.ToInt16(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u16:
						if (Convert.ToUInt16(value) > Convert.ToUInt16(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s32:
						if (Convert.ToInt32(value) > Convert.ToInt32(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u32:
						if (Convert.ToUInt32(value) > Convert.ToUInt32(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.f32:
						if (Convert.ToSingle(value) > Convert.ToSingle(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					}
				}
				if (field_inclusions != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
					{
						sbyte[] array12 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => sbyte.Parse(s));
						sbyte[] array19 = array12;
						foreach (sbyte array_value6 in array19)
						{
							if (Convert.ToSByte(value) != array_value6)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u8:
					{
						byte[] array14 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => byte.Parse(s));
						byte[] array20 = array14;
						foreach (byte array_value5 in array20)
						{
							if (Convert.ToByte(value) != array_value5)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s16:
					{
						short[] array8 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => short.Parse(s));
						short[] array17 = array8;
						foreach (short array_value4 in array17)
						{
							if (Convert.ToInt16(value) != array_value4)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u16:
					{
						ushort[] array6 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => ushort.Parse(s));
						for (int k = 0; k < array6.Length; k++)
						{
							short array_value3 = (short)array6[k];
							if (Convert.ToUInt16(value) != array_value3)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s32:
					{
						int[] array4 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => int.Parse(s));
						int[] array16 = array4;
						foreach (int array_value2 in array16)
						{
							if (Convert.ToInt32(value) != array_value2)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u32:
					{
						uint[] array10 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => uint.Parse(s));
						uint[] array18 = array10;
						foreach (uint array_value14 in array18)
						{
							if (Convert.ToUInt32(value) != array_value14)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.f32:
					{
						float[] array2 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => float.Parse(s));
						float[] array15 = array2;
						foreach (float array_value13 in array15)
						{
							if (Convert.ToSingle(value) != array_value13)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					}
				}
				if (field_exclusions != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
					{
						sbyte[] array11 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => sbyte.Parse(s));
						sbyte[] array25 = array11;
						foreach (sbyte array_value12 in array25)
						{
							if (Convert.ToSByte(value) == array_value12)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u8:
					{
						byte[] array13 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => byte.Parse(s));
						byte[] array26 = array13;
						foreach (byte array_value11 in array26)
						{
							if (Convert.ToByte(value) == array_value11)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s16:
					{
						short[] array7 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => short.Parse(s));
						short[] array23 = array7;
						foreach (short array_value10 in array23)
						{
							if (Convert.ToInt16(value) == array_value10)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u16:
					{
						ushort[] array5 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => ushort.Parse(s));
						for (int j = 0; j < array5.Length; j++)
						{
							short array_value9 = (short)array5[j];
							if (Convert.ToUInt16(value) == array_value9)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s32:
					{
						int[] array3 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => int.Parse(s));
						int[] array22 = array3;
						foreach (int array_value8 in array22)
						{
							if (Convert.ToInt32(value) == array_value8)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u32:
					{
						uint[] array9 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => uint.Parse(s));
						uint[] array24 = array9;
						foreach (uint array_value7 in array24)
						{
							if (Convert.ToUInt32(value) == array_value7)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.f32:
					{
						float[] array = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => float.Parse(s));
						float[] array21 = array;
						foreach (float array_value in array21)
						{
							if (Convert.ToSingle(value) == array_value)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					}
				}
				if (settings.ExportUniqueOnly)
				{
					if (unique_list.Contains(value))
					{
						isMatchedField = false;
					}
					if (!unique_list.Contains(value))
					{
						unique_list.Add(value);
					}
				}
				if (isMatchedField)
				{
					isValidRow = true;
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						row_line += Convert.ToSByte(value);
						break;
					case PARAMDEF.DefType.u8:
						row_line += Convert.ToByte(value);
						break;
					case PARAMDEF.DefType.s16:
						row_line += Convert.ToInt16(value);
						break;
					case PARAMDEF.DefType.u16:
						row_line += Convert.ToUInt16(value);
						break;
					case PARAMDEF.DefType.s32:
						row_line += Convert.ToInt32(value);
						break;
					case PARAMDEF.DefType.u32:
						row_line += Convert.ToUInt32(value);
						break;
					case PARAMDEF.DefType.f32:
						row_line += Convert.ToSingle(value);
						break;
					case PARAMDEF.DefType.fixstr:
					case PARAMDEF.DefType.fixstrW:
						row_line += value;
						break;
					}
				}
			}
			if (isValidRow)
			{
				output_file.WriteLine(row_line);
			}
		}
		output_file.Close();
		if (Settings.Default.UseTextEditor && Settings.Default.TextEditorPath != "")
		{
			try
			{
				Process.Start("\"" + Settings.Default.TextEditorPath + "\"", "\"" + Application.StartupPath + "\\" + field_data_path + "\"");
			}
			catch
			{
				SystemSounds.Hand.Play();
			}
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Field values exported to " + field_data_path, "Field Exporter", MessageBoxButtons.OK);
		}
	}

	private void rowReferenceFinderMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		RowReferenceSearch newFormReferenceFinder = new RowReferenceSearch();
		string log_dir = GetProjectDirectory("Logs");
		if (newFormReferenceFinder.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string reference_text = newFormReferenceFinder.GetReferenceText();
		if (reference_text == "")
		{
			MessageBox.Show("You did not specify a value.", "Reference Finder", MessageBoxButtons.OK);
			return;
		}
		string reference_file_path = log_dir + "\\\\RowReference.log";
		StreamWriter output_file = new StreamWriter(reference_file_path);
		foreach (ParamWrapper param in primary_result.ParamWrappers)
		{
			foreach (PARAM.Row row in param.Rows)
			{
				foreach (PARAM.Cell cell in row.Cells)
				{
					if (CheckFieldReference(reference_text, cell.Value.ToString()))
					{
						output_file.WriteLine(param.Name.ToString());
						if (row.Name != null)
						{
							output_file.WriteLine("  - Row Name: " + row.Name.ToString());
						}
						if (cell.Def.ToString() != null)
						{
							output_file.WriteLine("  - Cell Name: " + cell.Def.ToString());
						}
						output_file.WriteLine("  - Row ID: " + row.ID);
						output_file.WriteLine("  - Reference Value: " + reference_text);
						output_file.WriteLine("");
					}
				}
			}
		}
		output_file.Close();
		if (Settings.Default.UseTextEditor && Settings.Default.TextEditorPath != "")
		{
			try
			{
				Process.Start("\"" + Settings.Default.TextEditorPath + "\"", "\"" + Application.StartupPath + "\\" + reference_file_path + "\"");
			}
			catch
			{
				SystemSounds.Hand.Play();
			}
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("References exported to " + reference_file_path, "Reference Finder", MessageBoxButtons.OK);
		}
	}

	private void valueReferenceFinderMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		FieldReferenceSearch newFormReferenceFinder = new FieldReferenceSearch();
		string log_dir = GetProjectDirectory("Logs");
		if (newFormReferenceFinder.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		string reference_text = newFormReferenceFinder.GetReferenceText();
		if (reference_text == "")
		{
			MessageBox.Show("You did not specify a value.", "Reference Finder", MessageBoxButtons.OK);
			return;
		}
		string reference_file_path = log_dir + "\\\\ValueReference.log";
		StreamWriter output_file = new StreamWriter(reference_file_path);
		foreach (ParamWrapper param in primary_result.ParamWrappers)
		{
			foreach (PARAM.Row row in param.Rows)
			{
				if (CheckFieldReference(reference_text, row.ID.ToString()))
				{
					output_file.WriteLine(param.Name.ToString());
					if (row.Name != null)
					{
						output_file.WriteLine("  - Row Name: " + row.Name.ToString());
					}
					output_file.WriteLine("  - Row ID: " + row.ID);
					output_file.WriteLine("  - Reference Value: " + reference_text);
					output_file.WriteLine("");
				}
			}
		}
		output_file.Close();
		if (Settings.Default.UseTextEditor && Settings.Default.TextEditorPath != "")
		{
			try
			{
				Process.Start("\"" + Settings.Default.TextEditorPath + "\"", "\"" + Application.StartupPath + "\\" + reference_file_path + "\"");
			}
			catch
			{
				SystemSounds.Hand.Play();
			}
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("References exported to " + reference_file_path, "Reference Finder", MessageBoxButtons.OK);
		}
	}

	private bool CheckFieldReference(string value_1, string value_2)
	{
		if (int.TryParse(value_1, out var number))
		{
			if (int.TryParse(value_2, out number))
			{
				if (int.Parse(value_1).Equals(int.Parse(value_2)))
				{
					return true;
				}
				return false;
			}
			return false;
		}
		return false;
	}

	private void massImportDataMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode || MessageBox.Show("Mass Import will import data from CSV files for all params. Continue?", "Mass Import", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No || primary_result == null)
		{
			return;
		}
		foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
		{
			ImportParamData(wrapper, isSilent: true);
		}
		MessageBox.Show("Mass data import complete!", "Mass Import");
	}

	private void massExportDataMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode || MessageBox.Show("Mass Export will export all params to CSV. Continue?", "Mass Export", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No || (!Settings.Default.IncludeRowNameInCSV && MessageBox.Show("Row Names are currently not included. Continue?", "Mass Export", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No) || primary_result == null)
		{
			return;
		}
		foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
		{
			ExportParamData(wrapper, isSilent: true);
		}
		MessageBox.Show("Mass data export complete!", "Data Export");
	}

	private void fieldAdjusterMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		FieldAdjuster fieldAdjuster = new FieldAdjuster();
		string log_dir = GetProjectDirectory("Logs");
		if (fieldAdjuster.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		SaveParams(".bak");
		ParamWrapper paramWrapper = (ParamWrapper)rowSource.DataSource;
		string adjustment_file_path = log_dir + "\\\\FieldAdjustment_" + paramWrapper.Name + ".log";
		char delimiter = settings.ExportDelimiter.ToCharArray()[0];
		string field_match = Settings.Default.FieldAdjuster_FieldMatch;
		string row_range = Settings.Default.FieldAdjuster_RowRange;
		string row_partial_match = Settings.Default.FieldAdjuster_RowPartialMatch;
		string field_minimum = Settings.Default.FieldAdjuster_FieldMinimum;
		string field_maximum = Settings.Default.FieldAdjuster_FieldMaximum;
		string field_exclusions = Settings.Default.FieldAdjuster_FieldExclusion;
		string field_inclusions = Settings.Default.FieldAdjuster_FieldInclusion;
		string field_formula = Settings.Default.FieldAdjuster_Formula;
		string output_max = Settings.Default.FieldAdjuster_ValueMax;
		string output_min = Settings.Default.FieldAdjuster_ValueMin;
		if (field_match == "")
		{
			MessageBox.Show("You did not specify a target field.", "Field Adjuster", MessageBoxButtons.OK);
			return;
		}
		if (field_formula == "")
		{
			MessageBox.Show("You did not specify a field formula.", "Field Adjuster", MessageBoxButtons.OK);
			return;
		}
		StreamWriter output_file = new StreamWriter(adjustment_file_path);
		foreach (PARAM.Row row in paramWrapper.Rows)
		{
			if (row_range != "")
			{
				try
				{
					string[] row_range_array = row_range.Split(delimiter);
					if (row_range_array.Length != 2)
					{
						MessageBox.Show("Row range invalid.", "Field Adjuster", MessageBoxButtons.OK);
						output_file.Close();
						return;
					}
					if ((Regex.Matches(row_range_array[0], ".*[0-9].*").Count == 1 && int.Parse(row.ID.ToString()) < int.Parse(row_range_array[0].ToString())) || (Regex.Matches(row_range_array[1], ".*[0-9].*").Count == 1 && int.Parse(row.ID.ToString()) > int.Parse(row_range_array[1].ToString())))
					{
						continue;
					}
					goto IL_0279;
				}
				catch
				{
					MessageBox.Show("Row range invalid.", "Field Adjuster", MessageBoxButtons.OK);
					output_file.Close();
					return;
				}
			}
			goto IL_0279;
			IL_0279:
			if (row_partial_match != "")
			{
				int substring_length = row_partial_match.Length;
				string obj2 = row.ID.ToString();
				if (obj2.Substring(obj2.Length - substring_length) != row_partial_match)
				{
					continue;
				}
			}
			foreach (PARAM.Cell cell in row.Cells)
			{
				PARAMDEF.DefType type = cell.Def.DisplayType;
				string display_name = cell.Def.DisplayName;
				string internal_name = cell.Def.InternalName;
				string value = cell.Value.ToString();
				bool isMatchedField = false;
				if (type == PARAMDEF.DefType.dummy8 || ((!Settings.Default.CellView_ShowEditorNames || !(field_match == display_name)) && (Settings.Default.CellView_ShowEditorNames || !(field_match == internal_name))))
				{
					continue;
				}
				isMatchedField = true;
				_ = $"Entered value: {value} is invalid for [{cell.Name}] .";
				if (field_minimum != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						if (Convert.ToSByte(value) < Convert.ToSByte(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u8:
						if (Convert.ToByte(value) < Convert.ToByte(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s16:
						if (Convert.ToInt16(value) < Convert.ToInt16(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u16:
						if (Convert.ToUInt16(value) < Convert.ToUInt16(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s32:
						if (Convert.ToInt32(value) < Convert.ToInt32(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u32:
						if (Convert.ToUInt32(value) < Convert.ToUInt32(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.f32:
						if (Convert.ToSingle(value) < Convert.ToSingle(field_minimum))
						{
							isMatchedField = false;
						}
						break;
					}
				}
				if (field_maximum != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						if (Convert.ToSByte(value) > Convert.ToSByte(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u8:
						if (Convert.ToByte(value) > Convert.ToByte(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s16:
						if (Convert.ToInt16(value) > Convert.ToInt16(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u16:
						if (Convert.ToUInt16(value) > Convert.ToUInt16(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.s32:
						if (Convert.ToInt32(value) > Convert.ToInt32(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.u32:
						if (Convert.ToUInt32(value) > Convert.ToUInt32(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					case PARAMDEF.DefType.f32:
						if (Convert.ToSingle(value) > Convert.ToSingle(field_maximum))
						{
							isMatchedField = false;
						}
						break;
					}
				}
				if (field_inclusions != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
					{
						sbyte[] array12 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => sbyte.Parse(s));
						sbyte[] array19 = array12;
						foreach (sbyte array_value6 in array19)
						{
							if (Convert.ToSByte(value) != array_value6)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u8:
					{
						byte[] array14 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => byte.Parse(s));
						byte[] array20 = array14;
						foreach (byte array_value5 in array20)
						{
							if (Convert.ToByte(value) != array_value5)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s16:
					{
						short[] array8 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => short.Parse(s));
						short[] array17 = array8;
						foreach (short array_value4 in array17)
						{
							if (Convert.ToInt16(value) != array_value4)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u16:
					{
						ushort[] array6 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => ushort.Parse(s));
						for (int j = 0; j < array6.Length; j++)
						{
							short array_value3 = (short)array6[j];
							if (Convert.ToUInt16(value) != array_value3)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s32:
					{
						int[] array4 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => int.Parse(s));
						int[] array16 = array4;
						foreach (int array_value2 in array16)
						{
							if (Convert.ToInt32(value) != array_value2)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u32:
					{
						uint[] array10 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => uint.Parse(s));
						uint[] array18 = array10;
						foreach (uint array_value14 in array18)
						{
							if (Convert.ToUInt32(value) != array_value14)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.f32:
					{
						float[] array2 = Array.ConvertAll(field_inclusions.Split(delimiter), (string s) => float.Parse(s));
						float[] array15 = array2;
						foreach (float array_value13 in array15)
						{
							if (Convert.ToSingle(value) != array_value13)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					}
				}
				if (field_exclusions != "")
				{
					switch (type)
					{
					case PARAMDEF.DefType.s8:
					{
						sbyte[] array11 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => sbyte.Parse(s));
						sbyte[] array25 = array11;
						foreach (sbyte array_value12 in array25)
						{
							if (Convert.ToSByte(value) == array_value12)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u8:
					{
						byte[] array13 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => byte.Parse(s));
						byte[] array26 = array13;
						foreach (byte array_value11 in array26)
						{
							if (Convert.ToByte(value) == array_value11)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s16:
					{
						short[] array7 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => short.Parse(s));
						short[] array23 = array7;
						foreach (short array_value10 in array23)
						{
							if (Convert.ToInt16(value) == array_value10)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u16:
					{
						ushort[] array5 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => ushort.Parse(s));
						for (int i = 0; i < array5.Length; i++)
						{
							short array_value9 = (short)array5[i];
							if (Convert.ToUInt16(value) == array_value9)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.s32:
					{
						int[] array3 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => int.Parse(s));
						int[] array22 = array3;
						foreach (int array_value8 in array22)
						{
							if (Convert.ToInt32(value) == array_value8)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.u32:
					{
						uint[] array9 = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => uint.Parse(s));
						uint[] array24 = array9;
						foreach (uint array_value7 in array24)
						{
							if (Convert.ToUInt32(value) == array_value7)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					case PARAMDEF.DefType.f32:
					{
						float[] array = Array.ConvertAll(field_exclusions.Split(delimiter), (string s) => float.Parse(s));
						float[] array21 = array;
						foreach (float array_value in array21)
						{
							if (Convert.ToSingle(value) == array_value)
							{
								isMatchedField = false;
							}
						}
						break;
					}
					}
				}
				if (!isMatchedField)
				{
					continue;
				}
				output_file.WriteLine("Row: " + row.ID);
				output_file.WriteLine("- Field " + cell.Def.InternalName.ToString());
				output_file.WriteLine("- Old Value " + cell.Value.ToString());
				double field_result = 0.0;
				if (field_formula.Contains("x"))
				{
					string cell_formula = field_formula.Replace("x", cell.Value.ToString());
					field_result = new StringToFormula().Eval(cell_formula);
					if (output_max != "" && field_result > double.Parse(output_max))
					{
						field_result = double.Parse(output_max);
					}
					if (output_min != "" && field_result < double.Parse(output_min))
					{
						field_result = double.Parse(output_min);
					}
				}
				else
				{
					field_result = double.Parse(field_formula);
				}
				switch (type)
				{
				case PARAMDEF.DefType.s8:
					cell.Value = Convert.ToSByte(field_result);
					break;
				case PARAMDEF.DefType.u8:
					cell.Value = Convert.ToByte(field_result);
					break;
				case PARAMDEF.DefType.s16:
					cell.Value = Convert.ToInt16(field_result);
					break;
				case PARAMDEF.DefType.u16:
					cell.Value = Convert.ToUInt16(field_result);
					break;
				case PARAMDEF.DefType.s32:
					cell.Value = Convert.ToInt32(field_result);
					break;
				case PARAMDEF.DefType.u32:
					cell.Value = Convert.ToUInt32(field_result);
					break;
				case PARAMDEF.DefType.f32:
					cell.Value = Convert.ToSingle(field_result);
					break;
				}
				output_file.WriteLine("- New Value " + cell.Value.ToString());
				output_file.WriteLine("");
			}
		}
		output_file.Close();
		if (Settings.Default.UseTextEditor && Settings.Default.TextEditorPath != "")
		{
			try
			{
				Process.Start("\"" + Settings.Default.TextEditorPath + "\"", "\"" + Application.StartupPath + "\\" + adjustment_file_path + "\"");
			}
			catch
			{
				SystemSounds.Hand.Play();
			}
		}
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Field Adjustment complete.", "Field Adjuster", MessageBoxButtons.OK);
		}
	}

	private void affinityGeneratorMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			DataGridViewRow currentRow = dgvParams.CurrentRow;
			ParamWrapper wrapper = (ParamWrapper)currentRow.DataBoundItem;
			GameMode gameMode = (GameMode)toolStripComboBoxGame.SelectedItem;
			AffinityGeneration.GenerateAffinityRows(currentRow, wrapper, dgvRows, gameMode);
		}
	}

	private void ImportParamData(ParamWrapper wrapper, bool isSilent)
	{
		string projectDirectory = GetProjectDirectory("CSV");
		Utility.DebugPrint(wrapper.Name);
		string paramName = wrapper.Name;
		string paramFile = paramName + ".csv";
		string paramPath = projectDirectory + "\\" + paramFile;
		if (!File.Exists(paramPath))
		{
			if (!isSilent)
			{
				MessageBox.Show(paramFile + " does not exist.", "Import Data");
			}
		}
		else
		{
			if (!isSilent && MessageBox.Show("Importing will overwrite " + paramName + " data. Continue?", "Import Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				return;
			}
			StreamReader reader = null;
			try
			{
				reader = new StreamReader(File.OpenRead(paramPath));
			}
			catch (Exception ex)
			{
				Utility.ShowError($"Failed to open {paramFile}.\r\n\r\n{ex}");
				return;
			}
			reader.ReadLine();
			wrapper.Rows.Clear();
			while (!reader.EndOfStream)
			{
				string[] values = reader.ReadLine().Split(Settings.Default.ExportDelimiter.ToCharArray());
				int id = int.Parse(values[0], CultureInfo.InvariantCulture);
				string name = "";
				int cell_index = 1;
				if (Settings.Default.IncludeRowNameInCSV)
				{
					name = values[1];
					cell_index = 2;
				}
				PARAM.Row newRow = null;
				newRow = new PARAM.Row(id, name, wrapper.AppliedParamDef);
				wrapper.Rows.Add(newRow);
				wrapper.Rows.Sort((PARAM.Row r1, PARAM.Row r2) => r1.ID.CompareTo(r2.ID));
				foreach (PARAM.Cell cell in newRow.Cells)
				{
					PARAMDEF.DefType type = cell.Def.DisplayType;
					if (type == PARAMDEF.DefType.dummy8)
					{
						continue;
					}
					string new_value = values[cell_index];
					string exception_string = $"Row {newRow.ID}, Field {cell.Name} has invalid value {new_value}, skipped import of this value.";
					switch (type)
					{
					case PARAMDEF.DefType.s8:
						try
						{
							cell.Value = Convert.ToSByte(new_value, CultureInfo.InvariantCulture);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.u8:
						try
						{
							cell.Value = Convert.ToByte(new_value, CultureInfo.InvariantCulture);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.s16:
						try
						{
							cell.Value = Convert.ToInt16(new_value, CultureInfo.InvariantCulture);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.u16:
						try
						{
							cell.Value = Convert.ToUInt16(new_value, CultureInfo.InvariantCulture);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.s32:
						try
						{
							cell.Value = Convert.ToInt32(new_value, CultureInfo.InvariantCulture);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.u32:
						try
						{
							cell.Value = Convert.ToUInt32(new_value, CultureInfo.InvariantCulture);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.f32:
						try
						{
							cell.Value = Convert.ToSingle(new_value);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					case PARAMDEF.DefType.fixstr:
					case PARAMDEF.DefType.fixstrW:
						try
						{
							cell.Value = Convert.ToString(new_value);
						}
						catch
						{
							MessageBox.Show(exception_string, "Data Import");
						}
						break;
					}
					cell_index++;
				}
			}
			reader.Close();
			if (!Settings.Default.ShowConfirmationMessages && !isSilent)
			{
				MessageBox.Show(paramName + " data import complete!", "Data Import");
			}
		}
	}

	private void ExportParamData(ParamWrapper wrapper, bool isSilent)
	{
		string projectDirectory = GetProjectDirectory("CSV");
		string paramName = wrapper.Name;
		string paramFile = paramName + ".csv";
		string paramPath = projectDirectory + "\\" + paramFile;
		if (File.Exists(paramPath) && !isSilent)
		{
			if (MessageBox.Show(paramFile + " exists. Overwrite?", "Export Data", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				return;
			}
		}
		else if (!File.Exists(paramPath))
		{
			using FileStream fs = File.Create(paramPath);
			for (byte i = 0; i < 100; i = (byte)(i + 1))
			{
				fs.WriteByte(i);
			}
		}
		StreamWriter output_file = new StreamWriter(paramPath);
		string composed_line = "";
		if (settings.IncludeHeaderInCSV && !Settings.Default.VerboseCSVExport)
		{
			PARAM.Row first_row = wrapper.Rows.ElementAt(0);
			composed_line = ((!Settings.Default.IncludeRowNameInCSV) ? (composed_line + "Row ID" + settings.ExportDelimiter) : (composed_line + "Row ID" + settings.ExportDelimiter + "Row Name" + settings.ExportDelimiter));
			int cell_idx3 = 0;
			foreach (PARAM.Cell cell3 in first_row.Cells)
			{
				if (cell3.Def.DisplayType != PARAMDEF.DefType.dummy8)
				{
					composed_line = ((first_row.Cells.Count != cell_idx3) ? (composed_line + cell3.Def.InternalName + settings.ExportDelimiter) : (composed_line + cell3.Def.InternalName));
					cell_idx3++;
				}
			}
			char[] charsToTrim = settings.ExportDelimiter.ToCharArray();
			composed_line = composed_line.TrimEnd(charsToTrim);
			output_file.WriteLine(composed_line);
		}
		if (Settings.Default.VerboseCSVExport)
		{
			output_file.WriteLine("UNFURLED");
		}
		if (Settings.Default.VerboseCSVExport)
		{
			foreach (PARAM.Row row2 in wrapper.Rows)
			{
				output_file.WriteLine(row2.ID + settings.ExportDelimiter);
				output_file.WriteLine("~#" + row2.Name + settings.ExportDelimiter);
				int cell_idx2 = 0;
				foreach (PARAM.Cell cell2 in row2.Cells)
				{
					if (cell2.Def.DisplayType != PARAMDEF.DefType.dummy8)
					{
						if (row2.Cells.Count == cell_idx2)
						{
							output_file.WriteLine("~#" + cell2.Value.ToString());
						}
						else
						{
							output_file.WriteLine("~#" + cell2.Value.ToString() + settings.ExportDelimiter);
						}
					}
					cell_idx2++;
				}
			}
		}
		else
		{
			foreach (PARAM.Row row in wrapper.Rows)
			{
				composed_line = row.ID + settings.ExportDelimiter;
				if (settings.IncludeRowNameInCSV)
				{
					string row_name = row.Name;
					composed_line = composed_line + row_name + settings.ExportDelimiter;
				}
				int cell_idx = 0;
				foreach (PARAM.Cell cell in row.Cells)
				{
					if (cell.Def.DisplayType != PARAMDEF.DefType.dummy8)
					{
						composed_line = ((row.Cells.Count != cell_idx) ? (composed_line + cell.Value.ToString() + settings.ExportDelimiter) : (composed_line + cell.Value.ToString()));
					}
					cell_idx++;
				}
				settings.ExportDelimiter.ToCharArray();
				output_file.WriteLine(composed_line);
			}
		}
		output_file.Close();
		if (Settings.Default.UseTextEditor && Settings.Default.TextEditorPath != "" && !isSilent)
		{
			try
			{
				Process.Start("\"" + Settings.Default.TextEditorPath + "\"", "\"" + Application.StartupPath + "\\" + paramPath + "\"");
			}
			catch
			{
				SystemSounds.Hand.Play();
			}
		}
		if (!Settings.Default.ShowConfirmationMessages && !isSilent)
		{
			MessageBox.Show(paramName + " data export complete!", "Data Export");
		}
	}

	private void dgvParams_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void DgvParams_SelectionChanged(object sender, EventArgs e)
	{
		if (rowSource.DataSource != null)
		{
			ParamWrapper paramFile2 = (ParamWrapper)rowSource.DataSource;
			(int, int) indices2 = (0, 0);
			if (dgvRows.SelectedCells.Count > 0)
			{
				indices2.Item1 = dgvRows.SelectedCells[0].RowIndex;
			}
			else if (dgvRows.FirstDisplayedScrollingRowIndex >= 0)
			{
				indices2.Item1 = dgvRows.FirstDisplayedScrollingRowIndex;
			}
			if (dgvCells.FirstDisplayedScrollingRowIndex >= 0)
			{
				indices2.Item2 = dgvCells.FirstDisplayedScrollingRowIndex;
			}
			dgvIndices[paramFile2.Name] = indices2;
		}
		rowSource.DataSource = null;
		dgvCells.DataSource = null;
		if (dgvParams.SelectedCells.Count > 0)
		{
			settings.SelectedParam = dgvParams.SelectedCells[0].RowIndex;
			ParamWrapper paramFile = (ParamWrapper)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
			rowSource.DataMember = "Rows";
			rowSource.DataSource = paramFile;
			(int, int) indices = dgvIndices[paramFile.Name];
			if (indices.Item1 >= dgvRows.RowCount)
			{
				indices.Item1 = dgvRows.RowCount - 1;
			}
			if (indices.Item1 < 0)
			{
				indices.Item1 = 0;
			}
			dgvIndices[paramFile.Name] = indices;
			dgvRows.ClearSelection();
			if (dgvRows.RowCount > 0)
			{
				dgvRows.FirstDisplayedScrollingRowIndex = indices.Item1;
				dgvRows.Rows[indices.Item1].Selected = true;
			}
		}
	}

	private void DgvParams_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
	{
		if (e.RowIndex >= 0)
		{
			ParamWrapper paramWrapper = (ParamWrapper)dgvParams.Rows[e.RowIndex].DataBoundItem;
			e.ToolTipText = paramWrapper.Description;
		}
	}

	private void DgvRows_SelectionChanged(object sender, EventArgs e)
	{
		if (dgvRows.SelectedCells.Count <= 0)
		{
			return;
		}
		settings.SelectedRow = dgvRows.SelectedCells[0].RowIndex;
		ParamWrapper paramFile = (ParamWrapper)dgvParams.SelectedCells[0].OwningRow.DataBoundItem;
		(int, int) indices = dgvIndices[paramFile.Name];
		if (dgvCells.FirstDisplayedScrollingRowIndex >= 0)
		{
			indices.Item2 = dgvCells.FirstDisplayedScrollingRowIndex;
		}
		PARAM.Row row = (PARAM.Row)dgvRows.SelectedCells[0].OwningRow.DataBoundItem;
		dgvCells.DataSource = row.Cells.Where((PARAM.Cell cell) => cell.Def.DisplayType != PARAMDEF.DefType.dummy8).ToArray();
		if (indices.Item2 >= dgvCells.RowCount)
		{
			indices.Item2 = dgvCells.RowCount - 1;
		}
		if (indices.Item2 < 0)
		{
			indices.Item2 = 0;
		}
		dgvIndices[paramFile.Name] = indices;
		if (dgvCells.RowCount > 0)
		{
			dgvCells.FirstDisplayedScrollingRowIndex = indices.Item2;
		}
		for (int j = 0; j < dgvCells.Rows.Count; j++)
		{
			PARAM.Cell cell2 = (PARAM.Cell)dgvCells.Rows[j].DataBoundItem;
			if (!Settings.Default.ShowEnums || Settings.Default.ParamDifferenceMode || !tdf_dict.ContainsKey(cell2.Def.InternalType))
			{
				continue;
			}
			PARAMTDF tdf = tdf_dict[cell2.Def.InternalType];
			Dictionary<object, string> enum_dict = new Dictionary<object, string>();
			foreach (PARAMTDF.Entry entry in tdf.Entries)
			{
				if (Settings.Default.ShowEnumValueInName)
				{
					enum_dict.Add(entry.Value, entry.Value.ToString() + " - " + entry.Name);
				}
				else
				{
					enum_dict.Add(entry.Value, entry.Name);
				}
			}
			if (!enum_dict.ContainsKey(cell2.Value))
			{
				continue;
			}
			if (bool_type_tdfs.Contains(tdf.Name) && Settings.Default.DisplayBooleanEnumAsCheckbox)
			{
				dgvCells.Rows[j].Cells[FIELD_VALUE_COL] = new DataGridViewCheckBoxCell
				{
					TrueValue = "1",
					FalseValue = "0",
					ValueType = cell2.Value.GetType()
				};
			}
			else if (Settings.Default.DisableEnumForCustomTypes)
			{
				if (!custom_type_tdfs.Contains(tdf.Name))
				{
					dgvCells.Rows[j].Cells[FIELD_VALUE_COL] = new DataGridViewComboBoxCell
					{
						DataSource = enum_dict.ToArray(),
						DisplayMember = "Value",
						ValueMember = "Key",
						ValueType = cell2.Value.GetType()
					};
				}
			}
			else
			{
				dgvCells.Rows[j].Cells[FIELD_VALUE_COL] = new DataGridViewComboBoxCell
				{
					DataSource = enum_dict.ToArray(),
					DisplayMember = "Value",
					ValueMember = "Key",
					ValueType = cell2.Value.GetType()
				};
			}
		}
		Color int_color = Color.FromArgb(Settings.Default.FieldColor_Int_R, Settings.Default.FieldColor_Int_G, Settings.Default.FieldColor_Int_B);
		Color float_color = Color.FromArgb(Settings.Default.FieldColor_Float_R, Settings.Default.FieldColor_Float_G, Settings.Default.FieldColor_Float_B);
		Color bool_color = Color.FromArgb(Settings.Default.FieldColor_Bool_R, Settings.Default.FieldColor_Bool_G, Settings.Default.FieldColor_Bool_B);
		Color string_color = Color.FromArgb(Settings.Default.FieldColor_String_R, Settings.Default.FieldColor_String_G, Settings.Default.FieldColor_String_B);
		for (int i = 0; i < dgvCells.Rows.Count; i++)
		{
			DataGridViewRow cell3 = dgvCells.Rows[i];
			string type = cell3.Cells[FIELD_TYPE_COL].Value.ToString();
			cell3.Cells[2].Style.BackColor = Color.White;
			if (!Settings.Default.ParamDifferenceMode)
			{
				if (type.Contains("BOOL") || type.Contains("ON_OFF"))
				{
					cell3.Cells[2].Style.BackColor = bool_color;
				}
				else
				{
					switch (type)
					{
					case "u32":
					case "s32":
					case "u16":
					case "s16":
					case "u8":
					case "s8":
						cell3.Cells[2].Style.BackColor = int_color;
						break;
					case "f32":
						cell3.Cells[2].Style.BackColor = float_color;
						break;
					case "fixStr":
					case "fixStrW":
						cell3.Cells[2].Style.BackColor = string_color;
						break;
					default:
						cell3.Cells[2].Style.BackColor = int_color;
						break;
					}
				}
			}
			if (!Settings.Default.ParamDifferenceMode || secondary_result == null)
			{
				continue;
			}
			foreach (ParamWrapper secondary_wrapper in secondary_result.ParamWrappers)
			{
				if (!(secondary_wrapper.Name == paramFile.Name))
				{
					continue;
				}
				foreach (PARAM.Row secondary_row in secondary_wrapper.Rows)
				{
					if (row == null || secondary_row.ID != row.ID)
					{
						continue;
					}
					int dgvOffset = 0;
					for (int p = 0; p < secondary_row.Cells.Count; p++)
					{
						PARAM.Cell primary_cell = row.Cells[p];
						PARAM.Cell secondary_cell = secondary_row.Cells[p];
						if (primary_cell.Def.DisplayType != PARAMDEF.DefType.dummy8)
						{
							if (!primary_cell.Value.Equals(secondary_cell.Value))
							{
								dgvCells.Rows[p - dgvOffset].Cells[2].Style.BackColor = Color.Yellow;
							}
						}
						else
						{
							dgvOffset++;
						}
					}
				}
			}
		}
		ApplyCellFilter(invokeInvalidationMode: false);
	}

	private void DgvRows_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		if (e.ColumnIndex == ROW_ID_COL && (!int.TryParse((string)e.FormattedValue, out var value) || value < 0))
		{
			Utility.ShowError("Row ID must be a positive integer.\r\nEnter a valid number or press Esc to cancel.");
			e.Cancel = true;
		}
	}

	private void dgvRows_Scroll(object sender, ScrollEventArgs e)
	{
		rowContextMenu.Close();
	}

	private void dgvRows_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (InvalidationMode || e.ColumnIndex == -1 || e.RowIndex == -1 || e.Button != MouseButtons.Right)
		{
			return;
		}
		DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
		if (!c.Selected)
		{
			c.DataGridView.ClearSelection();
			c.DataGridView.CurrentCell = c;
			c.Selected = true;
		}
		DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
		if (currentCell != null)
		{
			ContextMenuStrip cms = currentCell.ContextMenuStrip;
			if (cms != null)
			{
				Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, cutOverflow: false);
				cms.Show(position: new Point(r.X + r.Width, r.Y + r.Height), control: currentCell.DataGridView);
			}
		}
	}

	private void dgvRows_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		_ = (DataGridView)sender;
		if (e.RowIndex != -1 && e.ColumnIndex != -1)
		{
			string row_param_name = dgvParams.CurrentCell.Value.ToString();
			if (row_param_name == ATKPARAM_PC || row_param_name == ATKPARAM_NPC || row_param_name == BEHAVIORPARAM_PC || row_param_name == BEHAVIORPARAM_NPC)
			{
				e.ContextMenuStrip = rowContextMenu;
			}
		}
	}

	private void rowContextMenu_Opening(object sender, CancelEventArgs e)
	{
		PARAM.Row row = (PARAM.Row)dgvRows.Rows[dgvRows.CurrentCell.RowIndex].DataBoundItem;
		string row_param_name = dgvParams.CurrentCell.Value.ToString();
		copyToParamMenuItem.Visible = false;
		copyToParamMenuItem.Text = "";
		if (row_param_name == ATKPARAM_PC)
		{
			copyToParamMenuItem.Visible = true;
			copyToParamMenuItem.Text = "Copy " + row.ID + " to " + ATKPARAM_NPC + ".";
		}
		else if (row_param_name == ATKPARAM_NPC)
		{
			copyToParamMenuItem.Visible = true;
			copyToParamMenuItem.Text = "Copy " + row.ID + " to " + ATKPARAM_PC + ".";
		}
		else if (row_param_name == BEHAVIORPARAM_PC)
		{
			copyToParamMenuItem.Visible = true;
			copyToParamMenuItem.Text = "Copy " + row.ID + " to " + BEHAVIORPARAM_NPC + ".";
		}
		else if (row_param_name == BEHAVIORPARAM_NPC)
		{
			copyToParamMenuItem.Visible = true;
			copyToParamMenuItem.Text = "Copy " + row.ID + " to " + BEHAVIORPARAM_PC + ".";
		}
	}

	private void copyToParamMenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Row row = (PARAM.Row)dgvRows.Rows[dgvRows.CurrentCell.RowIndex].DataBoundItem;
		string current_param_name = dgvParams.CurrentCell.Value.ToString();
		string target_param_name = "";
		ParamWrapper target_wrapper = null;
		if (current_param_name == ATKPARAM_PC)
		{
			target_param_name = ATKPARAM_NPC;
		}
		else if (current_param_name == ATKPARAM_NPC)
		{
			target_param_name = ATKPARAM_PC;
		}
		else if (current_param_name == BEHAVIORPARAM_PC)
		{
			target_param_name = BEHAVIORPARAM_NPC;
		}
		else if (current_param_name == BEHAVIORPARAM_NPC)
		{
			target_param_name = BEHAVIORPARAM_PC;
		}
		foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
		{
			if (wrapper.Name == target_param_name)
			{
				target_wrapper = wrapper;
			}
		}
		NewRow newRowForm = new NewRow("New Row", row.ID, row.Name);
		if (newRowForm.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		int id = newRowForm.ResultID;
		string name = newRowForm.ResultName;
		if (target_wrapper.Rows.Any((PARAM.Row wrapper_row) => row.ID == id))
		{
			Utility.ShowError($"A row with this ID already exists: {id}");
			return;
		}
		PARAM.Row row_result = new PARAM.Row(id, name, target_wrapper.AppliedParamDef);
		for (int k = 0; k < row.Cells.Count; k++)
		{
			row_result.Cells[k].Value = row.Cells[k].Value;
		}
		target_wrapper.Rows.Add(row_result);
		target_wrapper.Rows.Sort((PARAM.Row r1, PARAM.Row r2) => r1.ID.CompareTo(r2.ID));
		int target_param_idx = 0;
		int target_row_idx = 0;
		for (int i = 0; i < dgvParams.Rows.Count; i++)
		{
			if (dgvParams.Rows[i].Cells[0].Value.ToString() == target_param_name)
			{
				target_param_idx = i;
				dgvParams.ClearSelection();
				dgvParams.Rows[target_param_idx].Selected = true;
			}
		}
		for (int j = 0; j < dgvRows.Rows.Count; j++)
		{
			int value = Convert.ToInt32(dgvRows.Rows[j].Cells[0].Value);
			if (id == value)
			{
				target_row_idx = j;
				dgvRows.ClearSelection();
				dgvRows.Rows[target_row_idx].Selected = true;
				dgvRows.CurrentCell = dgvRows.Rows[target_row_idx].Cells[0];
			}
		}
	}

	private void dgvCells_SelectionChanged(object sender, EventArgs e)
	{
		if (dgvCells.SelectedCells.Count > 0)
		{
			settings.SelectedField = dgvCells.SelectedCells[0].RowIndex;
		}
	}

	private void dgvCells_Scroll(object sender, ScrollEventArgs e)
	{
		fieldContextMenu.Close();
	}

	private void DgvCells_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
	{
		if (Settings.Default.CellView_ShowEditorNames)
		{
			dgvCells.Columns[FIELD_PARAM_NAME_COL].Visible = false;
			dgvCells.Columns[FIELD_EDITOR_NAME_COL].Visible = true;
		}
		else
		{
			dgvCells.Columns[FIELD_PARAM_NAME_COL].Visible = true;
			dgvCells.Columns[FIELD_EDITOR_NAME_COL].Visible = false;
		}
		if (Settings.Default.CellView_ShowTypes)
		{
			dgvCells.Columns[FIELD_TYPE_COL].Visible = true;
			dgvCells.Columns[FIELD_TYPE_COL].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dgvCells.Columns[FIELD_VALUE_COL].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		}
		else
		{
			dgvCells.Columns[FIELD_TYPE_COL].Visible = false;
			dgvCells.Columns[FIELD_TYPE_COL].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dgvCells.Columns[FIELD_VALUE_COL].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		}
	}

	private void DgvCells_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
	{
	}

	private void DgvCells_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
	{
		if (!Settings.Default.EnableFieldValidation || e.ColumnIndex != FIELD_VALUE_COL)
		{
			return;
		}
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
		bool CheckValue = true;
		if (cell.Def.DisplayType == PARAMDEF.DefType.fixstr || cell.Def.DisplayType == PARAMDEF.DefType.fixstrW)
		{
			CheckValue = false;
		}
		if (Settings.Default.ShowEnums && tdf_dict.ContainsKey(cell.Def.InternalType))
		{
			CheckValue = false;
		}
		if (!CheckValue)
		{
			return;
		}
		float i = 0f;
		if (float.TryParse(e.FormattedValue.ToString(), out i))
		{
			float current_value = Convert.ToSingle(e.FormattedValue);
			float value_min = Convert.ToSingle(cell.Def.Minimum);
			float value_max = Convert.ToSingle(cell.Def.Maximum);
			if (current_value > value_max || current_value < value_min)
			{
				e.Cancel = true;
				EnterInvalidationMode();
				if (dgvCells.EditingPanel != null)
				{
					dgvCells.EditingPanel.BackColor = Color.Pink;
					dgvCells.EditingControl.BackColor = Color.Pink;
				}
				SystemSounds.Hand.Play();
			}
			else
			{
				ExitInvalidationMode();
			}
		}
		else
		{
			e.Cancel = true;
			EnterInvalidationMode();
			if (dgvCells.EditingPanel != null)
			{
				dgvCells.EditingPanel.BackColor = Color.Pink;
				dgvCells.EditingControl.BackColor = Color.Pink;
			}
			SystemSounds.Hand.Play();
		}
	}

	private void EnterInvalidationMode()
	{
		InvalidationMode = true;
		dgvParams.Enabled = false;
		dgvRows.Enabled = false;
		fileToolStripMenuItem.Enabled = false;
		editToolStripMenuItem.Enabled = false;
		viewToolStripMenuItem.Enabled = false;
		ToolStripMenuItem.Enabled = false;
		WorkflowToolStripMenuItem.Enabled = false;
		settingsMenuItem.Enabled = false;
		filter_Params.Enabled = false;
		button_FilterParams.Enabled = false;
		button_ResetFilterParams.Enabled = false;
		filter_Rows.Enabled = false;
		button_FilterRows.Enabled = false;
		button_ResetFilterRows.Enabled = false;
		filter_Cells.Enabled = false;
		button_FilterCells.Enabled = false;
		button_ResetFilterCells.Enabled = false;
	}

	private void ExitInvalidationMode()
	{
		InvalidationMode = false;
		dgvParams.Enabled = true;
		dgvRows.Enabled = true;
		fileToolStripMenuItem.Enabled = true;
		editToolStripMenuItem.Enabled = true;
		viewToolStripMenuItem.Enabled = true;
		ToolStripMenuItem.Enabled = true;
		WorkflowToolStripMenuItem.Enabled = true;
		settingsMenuItem.Enabled = true;
		filter_Params.Enabled = true;
		button_FilterParams.Enabled = true;
		button_ResetFilterParams.Enabled = true;
		filter_Rows.Enabled = true;
		button_FilterRows.Enabled = true;
		button_ResetFilterRows.Enabled = true;
		filter_Cells.Enabled = true;
		button_FilterCells.Enabled = true;
		button_ResetFilterCells.Enabled = true;
	}

	private void DgvCells_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
	{
	}

	private void DgvCells_DataError(object sender, DataGridViewDataErrorEventArgs e)
	{
		e.Cancel = true;
		if (dgvCells.EditingPanel != null)
		{
			dgvCells.EditingPanel.BackColor = Color.Pink;
			dgvCells.EditingControl.BackColor = Color.Pink;
		}
		SystemSounds.Hand.Play();
	}

	private void DgvCells_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
	{
		if ((e.ColumnIndex == FIELD_PARAM_NAME_COL || e.ColumnIndex == FIELD_EDITOR_NAME_COL) && e.RowIndex >= 0 && Settings.Default.ShowFieldDescriptions)
		{
			PARAM.Cell cell2 = (PARAM.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
			e.ToolTipText = cell2.Def.Description + "\nMinimum: " + cell2.Def.Minimum?.ToString() + "\nMaximum: " + cell2.Def.Maximum?.ToString() + "\nIncrement: " + cell2.Def.Increment;
		}
		if (e.ColumnIndex != FIELD_VALUE_COL || e.RowIndex < 0 || dgvRows.CurrentCell == null)
		{
			return;
		}
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
		PARAM.Row current_row = (PARAM.Row)dgvRows.Rows[dgvRows.CurrentCell.RowIndex].DataBoundItem;
		_ = current_row.ID;
		string tooltip = "";
		bool behaviorRow_FirstOnly = false;
		string cell_name = cell.Name.ToString();
		object cell_value = cell.Value;
		if (cell.ParamRef1 != null)
		{
			foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
			{
				if (cell.ParamRef1 != null && wrapper.Name.ToString() == cell.ParamRef1.ToString())
				{
					foreach (PARAM.Row row6 in wrapper.Rows)
					{
						string row_id6 = row6.ID.ToString();
						string row_name6 = ((row6.Name == null) ? "" : row6.Name.ToString());
						if (cell_name == "behaviorVariationId")
						{
							if (behaviorRow_FirstOnly)
							{
								continue;
							}
							foreach (PARAM.Cell field in row6.Cells)
							{
								if (field.Def.InternalName == "variationId" && Convert.ToInt32(field.Value) == Convert.ToInt32(cell_value))
								{
									tooltip = tooltip + row_id6 + " " + row_name6 + "\n";
									behaviorRow_FirstOnly = true;
								}
							}
						}
						else if (row6.ID == Convert.ToInt32(cell_value))
						{
							tooltip = tooltip + row_id6 + " " + row_name6 + "\n";
						}
					}
				}
				if (cell.ParamRef2 != null && wrapper.Name.ToString() == cell.ParamRef2.ToString())
				{
					foreach (PARAM.Row row5 in wrapper.Rows)
					{
						string row_id5 = row5.ID.ToString();
						string row_name5 = ((row5.Name == null) ? "" : row5.Name.ToString());
						if (row5.ID == Convert.ToInt32(cell_value))
						{
							tooltip = tooltip + row_id5 + " " + row_name5 + "\n";
						}
					}
				}
				if (cell.ParamRef3 != null && wrapper.Name.ToString() == cell.ParamRef3.ToString())
				{
					foreach (PARAM.Row row4 in wrapper.Rows)
					{
						string row_id4 = row4.ID.ToString();
						string row_name4 = ((row4.Name == null) ? "" : row4.Name.ToString());
						if (row4.ID == Convert.ToInt32(cell_value))
						{
							tooltip = tooltip + row_id4 + " " + row_name4 + "\n";
						}
					}
				}
				if (cell.ParamRef4 != null && wrapper.Name.ToString() == cell.ParamRef4.ToString())
				{
					foreach (PARAM.Row row3 in wrapper.Rows)
					{
						string row_id3 = row3.ID.ToString();
						string row_name3 = ((row3.Name == null) ? "" : row3.Name.ToString());
						if (row3.ID == Convert.ToInt32(cell_value))
						{
							tooltip = tooltip + row_id3 + " " + row_name3 + "\n";
						}
					}
				}
				if (cell.ParamRef5 != null && wrapper.Name.ToString() == cell.ParamRef5.ToString())
				{
					foreach (PARAM.Row row2 in wrapper.Rows)
					{
						string row_id2 = row2.ID.ToString();
						string row_name2 = ((row2.Name == null) ? "" : row2.Name.ToString());
						if (row2.ID == Convert.ToInt32(cell_value))
						{
							tooltip = tooltip + row_id2 + " " + row_name2 + "\n";
						}
					}
				}
				if (cell.ParamRef6 == null || !(wrapper.Name.ToString() == cell.ParamRef6.ToString()))
				{
					continue;
				}
				foreach (PARAM.Row row in wrapper.Rows)
				{
					string row_id = row.ID.ToString();
					string row_name = ((row.Name == null) ? "" : row.Name.ToString());
					if (row.ID == Convert.ToInt32(cell_value))
					{
						tooltip = tooltip + row_id + " " + row_name + "\n";
					}
				}
			}
		}
		if (Settings.Default.ParamDifferenceMode)
		{
			string current_param_name = dgvParams.CurrentCell.Value.ToString();
			if (secondary_result != null)
			{
				foreach (ParamWrapper secondary_wrapper in secondary_result.ParamWrappers)
				{
					if (!(secondary_wrapper.Name == current_param_name))
					{
						continue;
					}
					foreach (PARAM.Row secondary_row in secondary_wrapper.Rows)
					{
						if (current_row == null || secondary_row.ID != current_row.ID)
						{
							continue;
						}
						PARAM.Cell primary_cell = cell;
						for (int p = 0; p < secondary_row.Cells.Count; p++)
						{
							PARAM.Cell secondary_cell = secondary_row.Cells[p];
							if (primary_cell.Def.DisplayName == secondary_cell.Def.DisplayName && primary_cell.Def.DisplayType != PARAMDEF.DefType.dummy8 && !primary_cell.Value.Equals(secondary_cell.Value))
							{
								tooltip = tooltip + $"Secondary Value: {secondary_cell.Value}" + "\n";
							}
						}
					}
				}
			}
		}
		e.ToolTipText = tooltip;
	}

	private void dgvCells_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (InvalidationMode || e.ColumnIndex == -1 || e.RowIndex == -1 || e.Button != MouseButtons.Right)
		{
			return;
		}
		DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
		if (!c.Selected)
		{
			c.DataGridView.ClearSelection();
			c.DataGridView.CurrentCell = c;
			c.Selected = true;
		}
		DataGridViewCell currentCell = (sender as DataGridView).CurrentCell;
		if (currentCell != null)
		{
			ContextMenuStrip cms = currentCell.ContextMenuStrip;
			if (cms != null)
			{
				Rectangle r = currentCell.DataGridView.GetCellDisplayRectangle(currentCell.ColumnIndex, currentCell.RowIndex, cutOverflow: false);
				cms.Show(position: new Point(r.X + r.Width, r.Y + r.Height), control: currentCell.DataGridView);
			}
		}
	}

	private void dgvCells_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		_ = (DataGridView)sender;
		if (e.RowIndex != -1 && e.ColumnIndex != -1)
		{
			PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[e.RowIndex].DataBoundItem;
			if (cell.ParamRef1 != null && Convert.ToInt32(cell.Value) > -1)
			{
				e.ContextMenuStrip = fieldContextMenu;
			}
		}
	}

	private void fieldContextMenu_Opening(object sender, CancelEventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		int cell_value_id = Convert.ToInt32(cell.Value);
		bool behaviorRow_FirstOnly = false;
		GotoReference1MenuItem.Text = "";
		GotoReference2MenuItem.Text = "";
		GotoReference3MenuItem.Text = "";
		GotoReference4MenuItem.Text = "";
		GotoReference5MenuItem.Text = "";
		GotoReference6MenuItem.Text = "";
		GotoReference1MenuItem.Visible = false;
		GotoReference2MenuItem.Visible = false;
		GotoReference3MenuItem.Visible = false;
		GotoReference4MenuItem.Visible = false;
		GotoReference5MenuItem.Visible = false;
		GotoReference6MenuItem.Visible = false;
		foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
		{
			if (cell.ParamRef1 != null && wrapper.Name.ToString() == cell.ParamRef1.ToString())
			{
				foreach (PARAM.Row row6 in wrapper.Rows)
				{
					if (cell.Name.ToString() == "behaviorVariationId" || cell.Name.ToString() == "Behavior Variation ID")
					{
						if (behaviorRow_FirstOnly)
						{
							continue;
						}
						foreach (PARAM.Cell behavior_cell in row6.Cells)
						{
							if (behavior_cell.Def.InternalName == "variationId" && Convert.ToInt32(behavior_cell.Value) == cell_value_id)
							{
								GotoReference1MenuItem.Visible = true;
								GotoReference1MenuItem.Text = "Go to row " + row6.ID + " in " + cell.ParamRef1.ToString();
								behaviorRow_FirstOnly = true;
							}
						}
					}
					else if (row6.ID == cell_value_id)
					{
						GotoReference1MenuItem.Visible = true;
						GotoReference1MenuItem.Text = "Go to row " + row6.ID + " in " + cell.ParamRef1.ToString();
					}
				}
			}
			if (cell.ParamRef2 != null && wrapper.Name.ToString() == cell.ParamRef2.ToString())
			{
				foreach (PARAM.Row row5 in wrapper.Rows)
				{
					if (row5.ID == cell_value_id)
					{
						GotoReference2MenuItem.Visible = true;
						GotoReference2MenuItem.Text = "Go to row " + row5.ID + " in " + cell.ParamRef2.ToString();
					}
				}
			}
			if (cell.ParamRef3 != null && wrapper.Name.ToString() == cell.ParamRef3.ToString())
			{
				foreach (PARAM.Row row4 in wrapper.Rows)
				{
					if (row4.ID == cell_value_id)
					{
						GotoReference3MenuItem.Visible = true;
						GotoReference3MenuItem.Text = "Go to row " + row4.ID + " in " + cell.ParamRef3.ToString();
					}
				}
			}
			if (cell.ParamRef4 != null && wrapper.Name.ToString() == cell.ParamRef4.ToString())
			{
				foreach (PARAM.Row row3 in wrapper.Rows)
				{
					if (row3.ID == cell_value_id)
					{
						GotoReference4MenuItem.Visible = true;
						GotoReference4MenuItem.Text = "Go to row " + row3.ID + " in " + cell.ParamRef4.ToString();
					}
				}
			}
			if (cell.ParamRef5 != null && wrapper.Name.ToString() == cell.ParamRef5.ToString())
			{
				foreach (PARAM.Row row2 in wrapper.Rows)
				{
					if (row2.ID == cell_value_id)
					{
						GotoReference5MenuItem.Visible = true;
						GotoReference5MenuItem.Text = "Go to row " + row2.ID + " in " + cell.ParamRef5.ToString();
					}
				}
			}
			if (cell.ParamRef6 == null || !(wrapper.Name.ToString() == cell.ParamRef6.ToString()))
			{
				continue;
			}
			foreach (PARAM.Row row in wrapper.Rows)
			{
				if (row.ID == cell_value_id)
				{
					GotoReference6MenuItem.Visible = true;
					GotoReference6MenuItem.Text = "Go to row " + row.ID + " in " + cell.ParamRef6.ToString();
				}
			}
		}
	}

	private void GotoReferenceHelper(string paramref)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		int cell_value_id = Convert.ToInt32(cell.Value);
		bool isBehaviorReference = cell.Name.ToString() == "behaviorVariationId" || cell.Name.ToString() == "Behavior Variation ID";
		int target_param_idx = 0;
		int target_row_idx = 0;
		for (int i = 0; i < dgvParams.Rows.Count; i++)
		{
			string name = dgvParams.Rows[i].Cells[0].Value.ToString();
			if (paramref == name)
			{
				target_param_idx = i;
				dgvParams.ClearSelection();
				dgvParams.Rows[target_param_idx].Selected = true;
			}
		}
		if (isBehaviorReference)
		{
			int target_row = 0;
			bool isBehaviorMatched = false;
			foreach (ParamWrapper wrapper in primary_result.ParamWrappers)
			{
				if (!(wrapper.Name.ToString() == cell.ParamRef1.ToString()) || isBehaviorMatched)
				{
					continue;
				}
				foreach (PARAM.Row wrapper_row in wrapper.Rows)
				{
					if (isBehaviorMatched)
					{
						continue;
					}
					foreach (PARAM.Cell wrapper_cell in wrapper_row.Cells)
					{
						if (!isBehaviorMatched && (wrapper_cell.Name.ToString() == "variationId" || wrapper_cell.EditorName.ToString() == "Variation ID") && cell_value_id == Convert.ToInt32(wrapper_cell.Value))
						{
							target_row = wrapper_row.ID;
							isBehaviorMatched = true;
						}
					}
				}
			}
			for (int k = 0; k < dgvRows.Rows.Count; k++)
			{
				int value2 = Convert.ToInt32(dgvRows.Rows[k].Cells[0].Value);
				if (target_row == value2)
				{
					target_row_idx = k;
					dgvRows.ClearSelection();
					dgvRows.Rows[target_row_idx].Selected = true;
					dgvRows.CurrentCell = dgvRows.Rows[target_row_idx].Cells[0];
				}
			}
			return;
		}
		for (int j = 0; j < dgvRows.Rows.Count; j++)
		{
			int value = Convert.ToInt32(dgvRows.Rows[j].Cells[0].Value);
			if (cell_value_id == value)
			{
				target_row_idx = j;
				dgvRows.ClearSelection();
				dgvRows.Rows[target_row_idx].Selected = true;
				dgvRows.CurrentCell = dgvRows.Rows[target_row_idx].Cells[0];
			}
		}
	}

	private void GotoReference1MenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		GotoReferenceHelper(cell.ParamRef1.ToString());
	}

	private void GotoReference2MenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		GotoReferenceHelper(cell.ParamRef2.ToString());
	}

	private void GotoReference3MenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		GotoReferenceHelper(cell.ParamRef3.ToString());
	}

	private void GotoReference4MenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		GotoReferenceHelper(cell.ParamRef4.ToString());
	}

	private void GotoReference5MenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		GotoReferenceHelper(cell.ParamRef5.ToString());
	}

	private void GotoReference6MenuItem_Click(object sender, EventArgs e)
	{
		PARAM.Cell cell = (PARAM.Cell)dgvCells.Rows[dgvCells.CurrentCell.RowIndex].DataBoundItem;
		GotoReferenceHelper(cell.ParamRef6.ToString());
	}

	private void viewInterfaceSettingsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode && new InterfaceSettings().ShowDialog() == DialogResult.OK && !Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Interace Settings changed.", "Settings", MessageBoxButtons.OK);
		}
	}

	private void viewDataSettingsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode && new DataSettings().ShowDialog() == DialogResult.OK && !Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Data Settings changed.", "Settings", MessageBoxButtons.OK);
		}
	}

	private void viewFilterSettingsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode && new FilterSettings().ShowDialog() == DialogResult.OK && !Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Filter Settings changed.", "Settings", MessageBoxButtons.OK);
		}
	}

	private void selectSecondaryFileToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (!InvalidationMode)
		{
			secondaryFilePath.FileName = "";
			if (secondaryFilePath.ShowDialog() == DialogResult.OK)
			{
				Settings.Default.SecondaryFilePath = secondaryFilePath.FileName;
				LoadSecondaryParams(isSilent: false);
			}
		}
	}

	private void showParamDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (InvalidationMode)
		{
			return;
		}
		if (secondary_result == null)
		{
			MessageBox.Show("Secondary File not set.", "Secondary File", MessageBoxButtons.OK);
		}
		else if (Settings.Default.ParamDifferenceMode)
		{
			Settings.Default.ParamDifferenceMode = false;
			if (!Settings.Default.ShowConfirmationMessages)
			{
				MessageBox.Show("Param Difference mode coloring removed", "Param Difference Mode", MessageBoxButtons.OK);
			}
		}
		else
		{
			Settings.Default.ParamDifferenceMode = true;
			if (!Settings.Default.ShowConfirmationMessages)
			{
				MessageBox.Show("Param Difference mode coloring added", "Param Difference Mode", MessageBoxButtons.OK);
			}
		}
	}

	private void clearSecondaryFileToolMenuItem_Click(object sender, EventArgs e)
	{
		if (secondaryFilePath.FileName == "")
		{
			MessageBox.Show("Secondary File not set.", "Secondary File", MessageBoxButtons.OK);
			return;
		}
		secondaryFilePath.FileName = "";
		Settings.Default.SecondaryFilePath = secondaryFilePath.FileName;
		Settings.Default.ParamDifferenceMode = false;
		if (!Settings.Default.ShowConfirmationMessages)
		{
			MessageBox.Show("Removed set secondary file path.", "Secondary File", MessageBoxButtons.OK);
		}
	}

	private void button_FilterParams_Click(object sender, EventArgs e)
	{
		char[] command_delimiter = Settings.Default.Filter_CommandDelimiter.ToCharArray();
		char[] section_delimiter = Settings.Default.Filter_SectionDelimiter.ToCharArray();
		string command_delimiter_string = Settings.Default.Filter_CommandDelimiter;
		if (dgvParams.Rows.Count == 0)
		{
			return;
		}
		dgvParamsParamCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		EnterInvalidationMode();
		string[] input_list = filter_Params.Text.ToLower().Split(section_delimiter);
		if (input_list[0].Length < 1)
		{
			Utility.ShowError("No filter command present.");
			ExitInvalidationMode();
			return;
		}
		bool hasSelectedParam = false;
		if (dgvRows.Rows.Count > 0)
		{
			dgvRows.Rows[0].Selected = true;
		}
		if (dgvCells.Rows.Count > 0)
		{
			dgvCells.Rows[0].Selected = true;
		}
		for (int i = 0; i < dgvParams.Rows.Count; i++)
		{
			DataGridViewRow param = dgvParams.Rows[i];
			string param_name = param.Cells[0].Value.ToString().ToLower();
			CurrencyManager obj = (CurrencyManager)BindingContext[dgvParams.DataSource];
			obj.SuspendBinding();
			param.Visible = false;
			param.Selected = false;
			obj.ResumeBinding();
			List<bool> truth_list = new List<bool>();
			for (int j = 0; j < input_list.Length; j++)
			{
				truth_list.Add(item: false);
				string current_input = input_list[j];
				if (current_input.Contains("view" + command_delimiter_string))
				{
					current_input = current_input.Split(command_delimiter)[1].TrimStart(' ').ToLower();
					List<string> view_list = BuildViewList("Views\\\\Param\\\\", current_input);
					if (view_list.Count <= 0)
					{
						continue;
					}
					foreach (string view_name in view_list)
					{
						if (param_name.Contains(view_name))
						{
							truth_list[j] = true;
						}
					}
				}
				else if (current_input.Contains("exact" + command_delimiter_string))
				{
					current_input = current_input.Split(command_delimiter)[1].TrimStart(' ').ToLower();
					if (current_input.Length > 0 && param_name.Equals(current_input))
					{
						truth_list[j] = true;
					}
				}
				else if (current_input.Length > 0 && param_name.Contains(current_input))
				{
					truth_list[j] = true;
				}
			}
			if (truth_list.All((bool c) => c))
			{
				param.Visible = true;
				if (!hasSelectedParam)
				{
					hasSelectedParam = true;
					param.Selected = true;
				}
			}
		}
		dgvParamsParamCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		ExitInvalidationMode();
	}

	private void button_ResetFilterParams_Click(object sender, EventArgs e)
	{
		dgvParamsParamCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		bool hasSelectedFirstMatch = false;
		filter_Params.Text = "";
		for (int i = 0; i < dgvParams.Rows.Count; i++)
		{
			DataGridViewRow dgv_row = dgvParams.Rows[i];
			dgv_row.Visible = true;
			dgv_row.Selected = false;
			if (!hasSelectedFirstMatch)
			{
				dgv_row.Selected = true;
				hasSelectedFirstMatch = true;
			}
		}
		dgvParamsParamCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
	}

	private void button_FilterRows_Click(object sender, EventArgs e)
	{
		char[] command_delimiter = Settings.Default.Filter_CommandDelimiter.ToCharArray();
		char[] section_delimiter = Settings.Default.Filter_SectionDelimiter.ToCharArray();
		string command_delimiter_string = Settings.Default.Filter_CommandDelimiter;
		if (dgvRows.Rows.Count == 0)
		{
			return;
		}
		dgvRowsIDCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvRowsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		EnterInvalidationMode();
		string[] input_list = filter_Rows.Text.ToLower().Split(section_delimiter);
		if (input_list[0].Length < 1)
		{
			Utility.ShowError("No filter command present.");
			ExitInvalidationMode();
			return;
		}
		bool hasSelectedRow = false;
		for (int i = 0; i < dgvRows.Rows.Count; i++)
		{
			DataGridViewRow current_row = dgvRows.Rows[i];
			string current_row_name = "";
			if (current_row.Cells[ROW_NAME_COL].Value != null)
			{
				current_row_name = current_row.Cells[ROW_NAME_COL].Value.ToString().ToLower();
			}
			string current_row_id = current_row.Cells[ROW_ID_COL].Value.ToString().ToLower();
			PARAM.Row row_data = (PARAM.Row)current_row.DataBoundItem;
			CurrencyManager obj = (CurrencyManager)BindingContext[dgvRows.DataSource];
			obj.SuspendBinding();
			current_row.Visible = false;
			current_row.Selected = false;
			obj.ResumeBinding();
			List<bool> truth_list = new List<bool>();
			for (int j = 0; j < input_list.Length; j++)
			{
				truth_list.Add(item: false);
				string current_input = input_list[j];
				if (current_input.Contains("view" + command_delimiter_string))
				{
					current_input = current_input.Split(command_delimiter)[1].TrimStart(' ').ToLower();
					List<string> view_list = BuildViewList("Views\\\\Row\\\\", current_input);
					if (view_list.Count <= 0)
					{
						continue;
					}
					foreach (string view_name in view_list)
					{
						if (current_row_name.Contains(view_name) || current_row_id.Contains(view_name))
						{
							truth_list[j] = true;
						}
					}
				}
				else if (current_input.Contains("exact" + command_delimiter_string))
				{
					current_input = current_input.Split(command_delimiter)[1].TrimStart(' ').ToLower();
					if (current_input.Length > 0 && (current_row_name.Equals(current_input) || current_row_id.Equals(current_input)))
					{
						truth_list[j] = true;
					}
				}
				else if (current_input.Contains("field" + command_delimiter_string))
				{
					string[] temp_input = current_input.Split(command_delimiter);
					string field_input = temp_input[1].TrimStart(' ').ToLower();
					string value_input = temp_input[2].TrimStart(' ').ToLower();
					if (field_input.Length <= 0 || value_input.Length <= 0)
					{
						continue;
					}
					foreach (PARAM.Cell cell in row_data.Cells)
					{
						string field_editor_name = cell.EditorName.ToString().ToLower();
						string field_internal_name = cell.Name.ToString().ToLower();
						string field_value = cell.Value.ToString();
						if (!field_editor_name.Equals(field_input) && !field_internal_name.Equals(field_input))
						{
							continue;
						}
						if (value_input.Contains(">"))
						{
							float temp_float4 = Convert.ToSingle(value_input.Replace(">", ""));
							if (Convert.ToSingle(field_value) > temp_float4)
							{
								truth_list[j] = true;
							}
						}
						else if (value_input.Contains(">="))
						{
							float temp_float3 = Convert.ToSingle(value_input.Replace(">=", ""));
							if (Convert.ToSingle(field_value) >= temp_float3)
							{
								truth_list[j] = true;
							}
						}
						else if (value_input.Contains("<"))
						{
							float temp_float2 = Convert.ToSingle(value_input.Replace("<", ""));
							if (Convert.ToSingle(field_value) < temp_float2)
							{
								truth_list[j] = true;
							}
						}
						else if (value_input.Contains("<="))
						{
							float temp_float = Convert.ToSingle(value_input.Replace("<=", ""));
							if (Convert.ToSingle(field_value) <= temp_float)
							{
								truth_list[j] = true;
							}
						}
						else if (field_value.Equals(value_input))
						{
							truth_list[j] = true;
						}
					}
				}
				else if (current_input.Length > 0 && (current_row_name.Contains(current_input) || current_row_id.Contains(current_input)))
				{
					truth_list[j] = true;
				}
			}
			if (truth_list.All((bool c) => c))
			{
				current_row.Visible = true;
				if (!hasSelectedRow)
				{
					hasSelectedRow = true;
					current_row.Selected = true;
				}
			}
		}
		dgvRowsIDCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		dgvRowsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		ExitInvalidationMode();
	}

	private void button_ResetFilterRows_Click(object sender, EventArgs e)
	{
		dgvRowsIDCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvRowsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		bool hasSelectedFirstMatch = false;
		filter_Rows.Text = "";
		for (int i = 0; i < dgvRows.Rows.Count; i++)
		{
			DataGridViewRow dgv_row = dgvRows.Rows[i];
			dgv_row.Visible = true;
			dgv_row.Selected = false;
			if (!hasSelectedFirstMatch)
			{
				dgv_row.Selected = true;
				hasSelectedFirstMatch = true;
			}
		}
		dgvRowsIDCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		dgvRowsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
	}

	private void button_FilterCells_Click(object sender, EventArgs e)
	{
		ApplyCellFilter(invokeInvalidationMode: true);
	}

	private void ApplyCellFilter(bool invokeInvalidationMode)
	{
		char[] command_delimiter = Settings.Default.Filter_CommandDelimiter.ToCharArray();
		char[] section_delimiter = Settings.Default.Filter_SectionDelimiter.ToCharArray();
		string command_delimiter_string = Settings.Default.Filter_CommandDelimiter;
		if (dgvCells.Rows.Count == 0 || filter_Cells.Text == "")
		{
			return;
		}
		dgvCellsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvCellsEditorNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvCellsValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvCellsTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		if (invokeInvalidationMode)
		{
			EnterInvalidationMode();
		}
		string[] input_list = filter_Cells.Text.ToLower().Split(section_delimiter);
		if (input_list[0].Length > 0)
		{
			bool hasSelectedCell = false;
			for (int i = 0; i < dgvCells.Rows.Count; i++)
			{
				DataGridViewRow cell_row = dgvCells.Rows[i];
				string cell_row_param_name = cell_row.Cells[FIELD_PARAM_NAME_COL].Value.ToString().ToLower();
				string cell_row_editor_name = cell_row.Cells[FIELD_EDITOR_NAME_COL].Value.ToString().ToLower();
				string cell_row_value = cell_row.Cells[FIELD_VALUE_COL].Value.ToString().ToLower();
				CurrencyManager obj = (CurrencyManager)BindingContext[dgvCells.DataSource];
				obj.SuspendBinding();
				cell_row.Visible = false;
				cell_row.Selected = false;
				obj.ResumeBinding();
				List<bool> truth_list = new List<bool>();
				for (int j = 0; j < input_list.Length; j++)
				{
					truth_list.Add(item: false);
					string current_input = input_list[j];
					if (current_input.Contains("view" + command_delimiter_string))
					{
						current_input = current_input.Split(command_delimiter)[1].TrimStart(' ').ToLower();
						List<string> view_list = BuildViewList("Views\\\\Field\\\\", current_input);
						if (view_list.Count <= 0)
						{
							continue;
						}
						foreach (string view_name in view_list)
						{
							if (cell_row_param_name.Contains(view_name) || cell_row_editor_name.Contains(view_name) || cell_row_value.Contains(view_name))
							{
								truth_list[j] = true;
							}
						}
					}
					else if (current_input.Contains("exact" + command_delimiter_string))
					{
						current_input = current_input.Split(command_delimiter)[1].TrimStart(' ').ToLower();
						if (current_input.Length > 0 && (cell_row_param_name.Equals(current_input) || cell_row_editor_name.Equals(current_input) || cell_row_value.Equals(current_input)))
						{
							truth_list[j] = true;
						}
					}
					else if (current_input.Length > 0 && (cell_row_param_name.Contains(current_input) || cell_row_editor_name.Contains(current_input) || cell_row_value.Contains(current_input)))
					{
						truth_list[j] = true;
					}
				}
				if (truth_list.All((bool c) => c))
				{
					cell_row.Visible = true;
					if (!hasSelectedCell)
					{
						hasSelectedCell = true;
						cell_row.Selected = true;
					}
				}
			}
			dgvCellsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dgvCellsEditorNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			if (Settings.Default.CellView_ShowTypes)
			{
				dgvCellsValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
				dgvCellsTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			}
			else
			{
				dgvCellsValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
				dgvCellsTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			}
		}
		if (invokeInvalidationMode)
		{
			ExitInvalidationMode();
		}
	}

	private void button_ResetFilterCells_Click(object sender, EventArgs e)
	{
		dgvCellsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvCellsEditorNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvCellsValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		dgvCellsTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
		bool hasSelectedFirstMatch = false;
		filter_Cells.Text = "";
		for (int i = 0; i < dgvCells.Rows.Count; i++)
		{
			DataGridViewRow dgv_row = dgvCells.Rows[i];
			dgv_row.Visible = true;
			dgv_row.Selected = false;
			if (!hasSelectedFirstMatch)
			{
				dgv_row.Selected = true;
				hasSelectedFirstMatch = true;
			}
		}
		dgvCellsNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		dgvCellsEditorNameCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		if (Settings.Default.CellView_ShowTypes)
		{
			dgvCellsValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
			dgvCellsTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		}
		else
		{
			dgvCellsValueCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dgvCellsTypeCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
		}
	}

	private List<string> BuildViewList(string viewDir, string current_input)
	{
		List<string> names = new List<string>();
		if (!Directory.Exists(viewDir))
		{
			Utility.ShowError("Views directory not found.");
			return names;
		}
		FileInfo[] files = new DirectoryInfo(viewDir).GetFiles("*.txt");
		FileInfo[] array = files;
		foreach (FileInfo file in array)
		{
			if (!file.Name.ToLower().Contains(current_input))
			{
				continue;
			}
			using StreamReader reader = new StreamReader(file.FullName);
			while (!reader.EndOfStream)
			{
				string line = reader.ReadLine();
				names.Add(line.ToString().ToLower());
			}
		}
		return names;
	}

	private void toggleFilterVisibilityToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (Settings.Default.EnableFilterBar)
		{
			Settings.Default.EnableFilterBar = false;
		}
		else
		{
			Settings.Default.EnableFilterBar = true;
		}
		if (Settings.Default.EnableFilterBar)
		{
			menuStrip2.Visible = true;
			menuStrip3.Visible = true;
			menuStrip4.Visible = true;
		}
		else
		{
			menuStrip2.Visible = false;
			menuStrip3.Visible = false;
			menuStrip4.Visible = false;
		}
	}

	private void GenerateProjectDirectories(string project)
	{
		_ = (GameMode)toolStripComboBoxGame.SelectedItem;
		string projectDir = "Projects\\\\" + settings.ProjectName;
		bool num = Directory.Exists(projectDir);
		List<string> folders = new List<string> { "CSV", "Logs", "Names" };
		List<string> gametypes = new List<string> { "DS1", "DS1R", "DS2", "DS3", "SDT", "ER" };
		if (!num)
		{
			Directory.CreateDirectory(projectDir);
		}
		foreach (string folder in folders)
		{
			foreach (string type in gametypes)
			{
				string dir = projectDir + "\\" + folder + "\\" + type;
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
			}
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Chomp.Main));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.dataFileDialog = new System.Windows.Forms.OpenFileDialog();
		this.importedRegulation = new System.Windows.Forms.OpenFileDialog();
		this.menuStrip1 = new System.Windows.Forms.MenuStrip();
		this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripComboBoxGame = new System.Windows.Forms.ToolStripComboBox();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.exploreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ProjectFolderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.addRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.duplicateRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.deleteRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.findRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.findNextRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.gotoRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.findFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.findNextFieldToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toggleFieldNameTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toggleFieldTypeVisibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toggleFilterVisibilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.WorkflowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.fieldAdjusterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.affinityGeneratorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.logParamSizesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.importRowNames_Stock_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.importRowNames_Project_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.exportRowNames_Project_MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
		this.importDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.exportDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
		this.massImportDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massExportDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
		this.fieldExporterMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.valueReferenceFinderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.rowReferenceFinderMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.secondaryDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.selectSecondaryFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.clearSecondaryFileToolMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.showParamDifferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.settingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.viewSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.viewDataSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.viewInterfaceSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.splitContainer2 = new System.Windows.Forms.SplitContainer();
		this.dgvParams = new System.Windows.Forms.DataGridView();
		this.dgvParamsParamCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.menuStrip2 = new System.Windows.Forms.MenuStrip();
		this.filter_Params = new System.Windows.Forms.ToolStripTextBox();
		this.button_FilterParams = new System.Windows.Forms.ToolStripMenuItem();
		this.button_ResetFilterParams = new System.Windows.Forms.ToolStripMenuItem();
		this.splitContainer1 = new System.Windows.Forms.SplitContainer();
		this.dgvRows = new System.Windows.Forms.DataGridView();
		this.dgvRowsIDCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvRowsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.menuStrip3 = new System.Windows.Forms.MenuStrip();
		this.filter_Rows = new System.Windows.Forms.ToolStripTextBox();
		this.button_FilterRows = new System.Windows.Forms.ToolStripMenuItem();
		this.button_ResetFilterRows = new System.Windows.Forms.ToolStripMenuItem();
		this.dgvCells = new System.Windows.Forms.DataGridView();
		this.dgvCellsNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvCellsEditorNameCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvCellsValueCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.dgvCellsTypeCol = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.menuStrip4 = new System.Windows.Forms.MenuStrip();
		this.filter_Cells = new System.Windows.Forms.ToolStripTextBox();
		this.button_FilterCells = new System.Windows.Forms.ToolStripMenuItem();
		this.button_ResetFilterCells = new System.Windows.Forms.ToolStripMenuItem();
		this.statusStrip1 = new System.Windows.Forms.StatusStrip();
		this.processLabel = new System.Windows.Forms.ToolStripStatusLabel();
		this.fbdExport = new System.Windows.Forms.FolderBrowserDialog();
		this.fieldContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.GotoReference1MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.GotoReference2MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.GotoReference3MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.GotoReference4MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.GotoReference5MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.GotoReference6MenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.rowContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.copyToParamMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.secondaryFilePath = new System.Windows.Forms.OpenFileDialog();
		this.BottomToolStripPanel = new System.Windows.Forms.ToolStripPanel();
		this.TopToolStripPanel = new System.Windows.Forms.ToolStripPanel();
		this.RightToolStripPanel = new System.Windows.Forms.ToolStripPanel();
		this.LeftToolStripPanel = new System.Windows.Forms.ToolStripPanel();
		this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
		this.toolTip_filterParams = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_filterRows = new System.Windows.Forms.ToolTip(this.components);
		this.toolTip_filterCells = new System.Windows.Forms.ToolTip(this.components);
		this.viewFilterSettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.menuStrip1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).BeginInit();
		this.splitContainer2.Panel1.SuspendLayout();
		this.splitContainer2.Panel2.SuspendLayout();
		this.splitContainer2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvParams).BeginInit();
		this.menuStrip2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).BeginInit();
		this.splitContainer1.Panel1.SuspendLayout();
		this.splitContainer1.Panel2.SuspendLayout();
		this.splitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvRows).BeginInit();
		this.menuStrip3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCells).BeginInit();
		this.menuStrip4.SuspendLayout();
		this.statusStrip1.SuspendLayout();
		this.fieldContextMenu.SuspendLayout();
		this.rowContextMenu.SuspendLayout();
		base.SuspendLayout();
		this.dataFileDialog.FileName = "gameparam.parambnd.dcx";
		this.dataFileDialog.Filter = "Regulation or parambnd|*";
		this.importedRegulation.FileName = "regulation.bin";
		this.importedRegulation.Filter = "Regulation or parambnd|*";
		this.menuStrip1.BackColor = System.Drawing.Color.LightGray;
		this.menuStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.fileToolStripMenuItem, this.editToolStripMenuItem, this.viewToolStripMenuItem, this.WorkflowToolStripMenuItem, this.ToolStripMenuItem, this.secondaryDataToolStripMenuItem, this.settingsMenuItem });
		this.menuStrip1.Location = new System.Drawing.Point(2, 2);
		this.menuStrip1.Name = "menuStrip1";
		this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
		this.menuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.menuStrip1.Size = new System.Drawing.Size(973, 24);
		this.menuStrip1.TabIndex = 8;
		this.menuStrip1.Text = "menuStrip1";
		this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[9] { this.openToolStripMenuItem, this.saveToolStripMenuItem, this.restoreToolStripMenuItem, this.exportToolStripMenuItem, this.toolStripSeparator5, this.toolStripComboBoxGame, this.toolStripSeparator2, this.exploreToolStripMenuItem, this.ProjectFolderMenuItem });
		this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
		this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
		this.fileToolStripMenuItem.Text = "文件";
		this.openToolStripMenuItem.Name = "openToolStripMenuItem";
		this.openToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.O | System.Windows.Forms.Keys.Control;
		this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.openToolStripMenuItem.Text = "&打开文件";
		this.openToolStripMenuItem.ToolTipText = "Browse for a regulation file to edit.";
		this.openToolStripMenuItem.Click += new System.EventHandler(OpenToolStripMenuItem_Click);
		this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
		this.saveToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.S | System.Windows.Forms.Keys.Control;
		this.saveToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.saveToolStripMenuItem.Text = "&保存文件";
		this.saveToolStripMenuItem.ToolTipText = "Save changes to the regulation file.";
		this.saveToolStripMenuItem.Click += new System.EventHandler(SaveToolStripMenuItem_Click);
		this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
		this.restoreToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.restoreToolStripMenuItem.Text = "恢复文件";
		this.restoreToolStripMenuItem.ToolTipText = "还原regulation.bin备份文件.";
		this.restoreToolStripMenuItem.Click += new System.EventHandler(RestoreToolStripMenuItem_Click);
		this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
		this.exportToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.exportToolStripMenuItem.Text = "导出文件";
		this.exportToolStripMenuItem.ToolTipText = "Export an encrypted Data0.bdt to decrypted parambnds.";
		this.exportToolStripMenuItem.Click += new System.EventHandler(ExportToolStripMenuItem_Click);
		this.toolStripSeparator5.Name = "toolStripSeparator5";
		this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
		this.toolStripComboBoxGame.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.toolStripComboBoxGame.Name = "toolStripComboBoxGame";
		this.toolStripComboBoxGame.Size = new System.Drawing.Size(121, 23);
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(181, 6);
		this.exploreToolStripMenuItem.Name = "exploreToolStripMenuItem";
		this.exploreToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
		this.exploreToolStripMenuItem.Text = "Show Regulation File";
		this.exploreToolStripMenuItem.ToolTipText = "Open the regulation file directory in Explorer.";
		this.exploreToolStripMenuItem.Click += new System.EventHandler(ExploreToolStripMenuItem_Click);
		this.ProjectFolderMenuItem.Name = "ProjectFolderMenuItem";
		this.ProjectFolderMenuItem.Size = new System.Drawing.Size(184, 22);
		this.ProjectFolderMenuItem.Text = "View Project Folder";
		this.ProjectFolderMenuItem.Click += new System.EventHandler(ProjectFolderMenuItem_Click);
		this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.addRowToolStripMenuItem, this.duplicateRowToolStripMenuItem, this.deleteRowToolStripMenuItem, this.findRowToolStripMenuItem, this.findNextRowToolStripMenuItem, this.gotoRowToolStripMenuItem, this.findFieldToolStripMenuItem, this.findNextFieldToolStripMenuItem });
		this.editToolStripMenuItem.Name = "editToolStripMenuItem";
		this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
		this.editToolStripMenuItem.Text = "Edit";
		this.addRowToolStripMenuItem.Name = "addRowToolStripMenuItem";
		this.addRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.N | System.Windows.Forms.Keys.Control;
		this.addRowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.addRowToolStripMenuItem.Text = "Add Row";
		this.addRowToolStripMenuItem.ToolTipText = "Add a new row to the active param.";
		this.addRowToolStripMenuItem.Click += new System.EventHandler(AddRowToolStripMenuItem_Click);
		this.duplicateRowToolStripMenuItem.Name = "duplicateRowToolStripMenuItem";
		this.duplicateRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.N | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.duplicateRowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.duplicateRowToolStripMenuItem.Text = "Duplicate Row";
		this.duplicateRowToolStripMenuItem.ToolTipText = "Create a new row with values identical to the selected one";
		this.duplicateRowToolStripMenuItem.Click += new System.EventHandler(DuplicateRowToolStripMenuItem_Click);
		this.deleteRowToolStripMenuItem.Name = "deleteRowToolStripMenuItem";
		this.deleteRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete | System.Windows.Forms.Keys.Control;
		this.deleteRowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.deleteRowToolStripMenuItem.Text = "Delete Row";
		this.deleteRowToolStripMenuItem.ToolTipText = "Delete the currently selected row";
		this.deleteRowToolStripMenuItem.Click += new System.EventHandler(DeleteRowToolStripMenuItem_Click);
		this.findRowToolStripMenuItem.Name = "findRowToolStripMenuItem";
		this.findRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Control;
		this.findRowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.findRowToolStripMenuItem.Text = "&Find Row";
		this.findRowToolStripMenuItem.ToolTipText = "Search for a row with a matching name";
		this.findRowToolStripMenuItem.Click += new System.EventHandler(FindRowToolStripMenuItem_Click);
		this.findNextRowToolStripMenuItem.Name = "findNextRowToolStripMenuItem";
		this.findNextRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.K | System.Windows.Forms.Keys.Control;
		this.findNextRowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.findNextRowToolStripMenuItem.Text = "Find Next Row";
		this.findNextRowToolStripMenuItem.ToolTipText = "Search again with the previous pattern";
		this.findNextRowToolStripMenuItem.Click += new System.EventHandler(FindNextRowToolStripMenuItem_Click);
		this.gotoRowToolStripMenuItem.Name = "gotoRowToolStripMenuItem";
		this.gotoRowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.G | System.Windows.Forms.Keys.Control;
		this.gotoRowToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.gotoRowToolStripMenuItem.Text = "&Goto Row";
		this.gotoRowToolStripMenuItem.ToolTipText = "Go to a row with a certain ID";
		this.gotoRowToolStripMenuItem.Click += new System.EventHandler(GotoRowToolStripMenuItem_Click);
		this.findFieldToolStripMenuItem.Name = "findFieldToolStripMenuItem";
		this.findFieldToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F | System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Control;
		this.findFieldToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.findFieldToolStripMenuItem.Text = "Find Field";
		this.findFieldToolStripMenuItem.ToolTipText = "Search for a field with a matching name";
		this.findFieldToolStripMenuItem.Click += new System.EventHandler(FindFieldToolStripMenuItem_Click);
		this.findNextFieldToolStripMenuItem.Name = "findNextFieldToolStripMenuItem";
		this.findNextFieldToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3 | System.Windows.Forms.Keys.Shift;
		this.findNextFieldToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
		this.findNextFieldToolStripMenuItem.Text = "Find Next Field";
		this.findNextFieldToolStripMenuItem.ToolTipText = "Search again with the previous pattern";
		this.findNextFieldToolStripMenuItem.Click += new System.EventHandler(FindNextFieldToolStripMenuItem_Click);
		this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toggleFieldNameTypeToolStripMenuItem, this.toggleFieldTypeVisibilityToolStripMenuItem, this.toggleFilterVisibilityToolStripMenuItem });
		this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
		this.viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
		this.viewToolStripMenuItem.Text = "View";
		this.toggleFieldNameTypeToolStripMenuItem.Name = "toggleFieldNameTypeToolStripMenuItem";
		this.toggleFieldNameTypeToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
		this.toggleFieldNameTypeToolStripMenuItem.Text = "Toggle Field Name Scheme";
		this.toggleFieldNameTypeToolStripMenuItem.Click += new System.EventHandler(toggleFieldNameTypeToolStripMenuItem_Click);
		this.toggleFieldTypeVisibilityToolStripMenuItem.Name = "toggleFieldTypeVisibilityToolStripMenuItem";
		this.toggleFieldTypeVisibilityToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
		this.toggleFieldTypeVisibilityToolStripMenuItem.Text = "Toggle Field Type Visibility";
		this.toggleFieldTypeVisibilityToolStripMenuItem.Click += new System.EventHandler(toggleFieldTypeVisibilityToolStripMenuItem_Click);
		this.toggleFilterVisibilityToolStripMenuItem.Name = "toggleFilterVisibilityToolStripMenuItem";
		this.toggleFilterVisibilityToolStripMenuItem.Size = new System.Drawing.Size(217, 22);
		this.toggleFilterVisibilityToolStripMenuItem.Text = "Toggle Filter Visibility";
		this.toggleFilterVisibilityToolStripMenuItem.Click += new System.EventHandler(toggleFilterVisibilityToolStripMenuItem_Click);
		this.WorkflowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.fieldAdjusterMenuItem, this.affinityGeneratorMenuItem, this.logParamSizesToolStripMenuItem });
		this.WorkflowToolStripMenuItem.Name = "WorkflowToolStripMenuItem";
		this.WorkflowToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
		this.WorkflowToolStripMenuItem.Text = "Tools";
		this.fieldAdjusterMenuItem.AutoToolTip = true;
		this.fieldAdjusterMenuItem.Name = "fieldAdjusterMenuItem";
		this.fieldAdjusterMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
		this.fieldAdjusterMenuItem.Size = new System.Drawing.Size(213, 22);
		this.fieldAdjusterMenuItem.Text = "Field Adjuster";
		this.fieldAdjusterMenuItem.ToolTipText = "Use this to quickly apply a field edit to multiple rows.\r\nContains various tools to filter which rows are edited.";
		this.fieldAdjusterMenuItem.Click += new System.EventHandler(fieldAdjusterMenuItem_Click);
		this.affinityGeneratorMenuItem.AutoToolTip = true;
		this.affinityGeneratorMenuItem.Name = "affinityGeneratorMenuItem";
		this.affinityGeneratorMenuItem.ShortcutKeys = System.Windows.Forms.Keys.W | System.Windows.Forms.Keys.Control;
		this.affinityGeneratorMenuItem.Size = new System.Drawing.Size(213, 22);
		this.affinityGeneratorMenuItem.Text = "Affinity Generator";
		this.affinityGeneratorMenuItem.ToolTipText = "Use this to quickly generate rows for weapons\r\nthat lack affinities.\r\n\r\nYou can change the configuration applied by\r\nediting the text files in:\r\n.\\<gametype>\\Configuration\\AffinityGenerator\\";
		this.affinityGeneratorMenuItem.Click += new System.EventHandler(affinityGeneratorMenuItem_Click);
		this.logParamSizesToolStripMenuItem.Name = "logParamSizesToolStripMenuItem";
		this.logParamSizesToolStripMenuItem.Size = new System.Drawing.Size(213, 22);
		this.logParamSizesToolStripMenuItem.Text = "Log Param Sizes";
		this.logParamSizesToolStripMenuItem.Click += new System.EventHandler(logParamSizesToolStripMenuItem_Click);
		this.ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[14]
		{
			this.importRowNames_Stock_MenuItem, this.toolStripSeparator1, this.importRowNames_Project_MenuItem, this.exportRowNames_Project_MenuItem, this.toolStripSeparator8, this.importDataMenuItem, this.exportDataMenuItem, this.toolStripSeparator7, this.massImportDataMenuItem, this.massExportDataMenuItem,
			this.toolStripSeparator9, this.fieldExporterMenuItem, this.valueReferenceFinderMenuItem, this.rowReferenceFinderMenuItem
		});
		this.ToolStripMenuItem.Name = "ToolStripMenuItem";
		this.ToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
		this.ToolStripMenuItem.Text = "Field Data";
		this.importRowNames_Stock_MenuItem.AutoToolTip = true;
		this.importRowNames_Stock_MenuItem.Name = "importRowNames_Stock_MenuItem";
		this.importRowNames_Stock_MenuItem.Size = new System.Drawing.Size(235, 22);
		this.importRowNames_Stock_MenuItem.Text = "Import Stock Row Names";
		this.importRowNames_Stock_MenuItem.ToolTipText = "Import row names from Paramdex names.\r\n";
		this.importRowNames_Stock_MenuItem.Click += new System.EventHandler(importRowNames_Stock_MenuItem_Click);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(232, 6);
		this.importRowNames_Project_MenuItem.AutoToolTip = true;
		this.importRowNames_Project_MenuItem.Name = "importRowNames_Project_MenuItem";
		this.importRowNames_Project_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
		this.importRowNames_Project_MenuItem.Size = new System.Drawing.Size(235, 22);
		this.importRowNames_Project_MenuItem.Text = "Import Project Row Names";
		this.importRowNames_Project_MenuItem.ToolTipText = "Import row names from Project row names.\r\n\r\n";
		this.importRowNames_Project_MenuItem.Click += new System.EventHandler(importRowNames_Project_MenuItem_Click);
		this.exportRowNames_Project_MenuItem.AutoToolTip = true;
		this.exportRowNames_Project_MenuItem.Name = "exportRowNames_Project_MenuItem";
		this.exportRowNames_Project_MenuItem.ShortcutKeys = System.Windows.Forms.Keys.F8;
		this.exportRowNames_Project_MenuItem.Size = new System.Drawing.Size(235, 22);
		this.exportRowNames_Project_MenuItem.Text = "Export Project Row Names";
		this.exportRowNames_Project_MenuItem.ToolTipText = "Export row names to Project row names.\r\n\r\n";
		this.exportRowNames_Project_MenuItem.Click += new System.EventHandler(exportRowNames_Project_MenuItem_Click);
		this.toolStripSeparator8.Name = "toolStripSeparator8";
		this.toolStripSeparator8.Size = new System.Drawing.Size(232, 6);
		this.importDataMenuItem.AutoToolTip = true;
		this.importDataMenuItem.Name = "importDataMenuItem";
		this.importDataMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F11;
		this.importDataMenuItem.Size = new System.Drawing.Size(235, 22);
		this.importDataMenuItem.Text = "Import Data";
		this.importDataMenuItem.ToolTipText = "For the currently selected param, \r\nimport param data from a CSV file of the same name.\r\n\r\nData files are found in .\\<gametype>\\Data\\";
		this.importDataMenuItem.Click += new System.EventHandler(importDataMenuItem_Click);
		this.exportDataMenuItem.AutoToolTip = true;
		this.exportDataMenuItem.Name = "exportDataMenuItem";
		this.exportDataMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F12;
		this.exportDataMenuItem.Size = new System.Drawing.Size(235, 22);
		this.exportDataMenuItem.Text = "Export Data";
		this.exportDataMenuItem.ToolTipText = "For the currently selected param, \r\nexport param data into a CSV file of the same name.\r\n\r\nData files are found in .\\<gametype>\\Data\\\r\n";
		this.exportDataMenuItem.Click += new System.EventHandler(exportDataMenuItem_Click);
		this.toolStripSeparator7.Name = "toolStripSeparator7";
		this.toolStripSeparator7.Size = new System.Drawing.Size(232, 6);
		this.massImportDataMenuItem.AutoToolTip = true;
		this.massImportDataMenuItem.Name = "massImportDataMenuItem";
		this.massImportDataMenuItem.Size = new System.Drawing.Size(235, 22);
		this.massImportDataMenuItem.Text = "Mass Import Data";
		this.massImportDataMenuItem.ToolTipText = "For all params, import param data from \r\na CSV file of the same name.\r\n\r\nData files are found in .\\<gametype>\\Data\\\r\n";
		this.massImportDataMenuItem.Click += new System.EventHandler(massImportDataMenuItem_Click);
		this.massExportDataMenuItem.AutoToolTip = true;
		this.massExportDataMenuItem.Name = "massExportDataMenuItem";
		this.massExportDataMenuItem.Size = new System.Drawing.Size(235, 22);
		this.massExportDataMenuItem.Text = "Mass Export Data";
		this.massExportDataMenuItem.ToolTipText = "For all params, export param data into a CSV file \r\nof the same name.\r\n\r\nData files are found in .\\<gametype>\\Data\\\r\n";
		this.massExportDataMenuItem.Click += new System.EventHandler(massExportDataMenuItem_Click);
		this.toolStripSeparator9.Name = "toolStripSeparator9";
		this.toolStripSeparator9.Size = new System.Drawing.Size(232, 6);
		this.fieldExporterMenuItem.AutoToolTip = true;
		this.fieldExporterMenuItem.Name = "fieldExporterMenuItem";
		this.fieldExporterMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
		this.fieldExporterMenuItem.Size = new System.Drawing.Size(235, 22);
		this.fieldExporterMenuItem.Text = "Field Exporter";
		this.fieldExporterMenuItem.ToolTipText = "Export the specific field values for a field.";
		this.fieldExporterMenuItem.Click += new System.EventHandler(fieldExporterMenuItem_Click);
		this.valueReferenceFinderMenuItem.AutoToolTip = true;
		this.valueReferenceFinderMenuItem.Name = "valueReferenceFinderMenuItem";
		this.valueReferenceFinderMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
		this.valueReferenceFinderMenuItem.Size = new System.Drawing.Size(235, 22);
		this.valueReferenceFinderMenuItem.Text = "Value Reference Finder";
		this.valueReferenceFinderMenuItem.ToolTipText = "Find all references to a field value.";
		this.valueReferenceFinderMenuItem.Click += new System.EventHandler(valueReferenceFinderMenuItem_Click);
		this.rowReferenceFinderMenuItem.AutoToolTip = true;
		this.rowReferenceFinderMenuItem.Name = "rowReferenceFinderMenuItem";
		this.rowReferenceFinderMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
		this.rowReferenceFinderMenuItem.Size = new System.Drawing.Size(235, 22);
		this.rowReferenceFinderMenuItem.Text = "Row Reference Finder";
		this.rowReferenceFinderMenuItem.ToolTipText = "Find all references to a row ID.";
		this.rowReferenceFinderMenuItem.Click += new System.EventHandler(rowReferenceFinderMenuItem_Click);
		this.secondaryDataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.selectSecondaryFileToolStripMenuItem, this.clearSecondaryFileToolMenuItem, this.showParamDifferencesToolStripMenuItem });
		this.secondaryDataToolStripMenuItem.Name = "secondaryDataToolStripMenuItem";
		this.secondaryDataToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
		this.secondaryDataToolStripMenuItem.Text = "File Data";
		this.selectSecondaryFileToolStripMenuItem.Name = "selectSecondaryFileToolStripMenuItem";
		this.selectSecondaryFileToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
		this.selectSecondaryFileToolStripMenuItem.Text = "Select Secondary File";
		this.selectSecondaryFileToolStripMenuItem.Click += new System.EventHandler(selectSecondaryFileToolStripMenuItem_Click);
		this.clearSecondaryFileToolMenuItem.Name = "clearSecondaryFileToolMenuItem";
		this.clearSecondaryFileToolMenuItem.Size = new System.Drawing.Size(237, 22);
		this.clearSecondaryFileToolMenuItem.Text = "Clear Secondary File";
		this.clearSecondaryFileToolMenuItem.Click += new System.EventHandler(clearSecondaryFileToolMenuItem_Click);
		this.showParamDifferencesToolStripMenuItem.Name = "showParamDifferencesToolStripMenuItem";
		this.showParamDifferencesToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
		this.showParamDifferencesToolStripMenuItem.Text = "Toggle Param Difference Mode";
		this.showParamDifferencesToolStripMenuItem.Click += new System.EventHandler(showParamDifferencesToolStripMenuItem_Click);
		this.settingsMenuItem.AutoToolTip = true;
		this.settingsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.viewSettingsToolStripMenuItem, this.viewDataSettingsToolStripMenuItem, this.viewInterfaceSettingsToolStripMenuItem, this.viewFilterSettingsToolStripMenuItem });
		this.settingsMenuItem.Name = "settingsMenuItem";
		this.settingsMenuItem.Size = new System.Drawing.Size(61, 20);
		this.settingsMenuItem.Text = "Settings";
		this.viewSettingsToolStripMenuItem.Name = "viewSettingsToolStripMenuItem";
		this.viewSettingsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
		this.viewSettingsToolStripMenuItem.Text = "View General Settings";
		this.viewSettingsToolStripMenuItem.ToolTipText = "Configure the settings used by Yapped for various features.";
		this.viewSettingsToolStripMenuItem.Click += new System.EventHandler(viewSettingsToolStripMenuItem_Click);
		this.viewDataSettingsToolStripMenuItem.Name = "viewDataSettingsToolStripMenuItem";
		this.viewDataSettingsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
		this.viewDataSettingsToolStripMenuItem.Text = "View Data Settings";
		this.viewDataSettingsToolStripMenuItem.Click += new System.EventHandler(viewDataSettingsToolStripMenuItem_Click);
		this.viewInterfaceSettingsToolStripMenuItem.Name = "viewInterfaceSettingsToolStripMenuItem";
		this.viewInterfaceSettingsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
		this.viewInterfaceSettingsToolStripMenuItem.Text = "View Interface Settings";
		this.viewInterfaceSettingsToolStripMenuItem.Click += new System.EventHandler(viewInterfaceSettingsToolStripMenuItem_Click);
		this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.splitContainer2.Location = new System.Drawing.Point(2, 26);
		this.splitContainer2.Name = "splitContainer2";
		this.splitContainer2.Panel1.Controls.Add(this.dgvParams);
		this.splitContainer2.Panel1.Controls.Add(this.menuStrip2);
		this.splitContainer2.Panel2.Controls.Add(this.splitContainer1);
		this.splitContainer2.Size = new System.Drawing.Size(973, 576);
		this.splitContainer2.SplitterDistance = 249;
		this.splitContainer2.TabIndex = 2;
		this.dgvParams.AllowUserToAddRows = false;
		this.dgvParams.AllowUserToDeleteRows = false;
		this.dgvParams.AllowUserToResizeColumns = false;
		this.dgvParams.AllowUserToResizeRows = false;
		this.dgvParams.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
		this.dgvParams.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvParams.Columns.AddRange(this.dgvParamsParamCol);
		this.dgvParams.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvParams.Location = new System.Drawing.Point(0, 31);
		this.dgvParams.MinimumSize = new System.Drawing.Size(160, 0);
		this.dgvParams.MultiSelect = false;
		this.dgvParams.Name = "dgvParams";
		this.dgvParams.RowHeadersVisible = false;
		this.dgvParams.RowHeadersWidth = 51;
		this.dgvParams.Size = new System.Drawing.Size(249, 545);
		this.dgvParams.TabIndex = 0;
		this.dgvParams.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgvParams_CellContentClick);
		this.dgvParams.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(DgvParams_CellToolTipTextNeeded);
		this.dgvParams.SelectionChanged += new System.EventHandler(DgvParams_SelectionChanged);
		this.dgvParamsParamCol.DataPropertyName = "Name";
		this.dgvParamsParamCol.HeaderText = "参数";
		this.dgvParamsParamCol.MinimumWidth = 6;
		this.dgvParamsParamCol.Name = "dgvParamsParamCol";
		this.dgvParamsParamCol.ReadOnly = true;
		this.menuStrip2.BackColor = System.Drawing.Color.Transparent;
		this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.filter_Params, this.button_FilterParams, this.button_ResetFilterParams });
		this.menuStrip2.Location = new System.Drawing.Point(0, 0);
		this.menuStrip2.Name = "menuStrip2";
		this.menuStrip2.Size = new System.Drawing.Size(249, 31);
		this.menuStrip2.TabIndex = 1;
		this.menuStrip2.Text = "menuStrip2";
		this.filter_Params.AutoToolTip = true;
		this.filter_Params.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.filter_Params.Font = new System.Drawing.Font("Segoe UI", 9f);
		this.filter_Params.Margin = new System.Windows.Forms.Padding(2);
		this.filter_Params.Name = "filter_Params";
		this.filter_Params.Size = new System.Drawing.Size(120, 23);
		this.filter_Params.ToolTipText = resources.GetString("filter_Params.ToolTipText");
		this.button_FilterParams.AutoToolTip = true;
		this.button_FilterParams.BackColor = System.Drawing.Color.DarkGray;
		this.button_FilterParams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_FilterParams.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.button_FilterParams.Margin = new System.Windows.Forms.Padding(2);
		this.button_FilterParams.Name = "button_FilterParams";
		this.button_FilterParams.Size = new System.Drawing.Size(50, 23);
		this.button_FilterParams.Text = "完成";
		this.button_FilterParams.ToolTipText = "Apply param view filter.";
		this.button_FilterParams.Click += new System.EventHandler(button_FilterParams_Click);
		this.button_ResetFilterParams.AutoToolTip = true;
		this.button_ResetFilterParams.BackColor = System.Drawing.Color.DarkGray;
		this.button_ResetFilterParams.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_ResetFilterParams.Margin = new System.Windows.Forms.Padding(2);
		this.button_ResetFilterParams.Name = "button_ResetFilterParams";
		this.button_ResetFilterParams.Size = new System.Drawing.Size(47, 23);
		this.button_ResetFilterParams.Text = "重设";
		this.button_ResetFilterParams.ToolTipText = "Reset param view.";
		this.button_ResetFilterParams.Click += new System.EventHandler(button_ResetFilterParams_Click);
		this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.splitContainer1.Location = new System.Drawing.Point(0, 0);
		this.splitContainer1.Name = "splitContainer1";
		this.splitContainer1.Panel1.Controls.Add(this.dgvRows);
		this.splitContainer1.Panel1.Controls.Add(this.menuStrip3);
		this.splitContainer1.Panel2.Controls.Add(this.dgvCells);
		this.splitContainer1.Panel2.Controls.Add(this.menuStrip4);
		this.splitContainer1.Size = new System.Drawing.Size(720, 576);
		this.splitContainer1.SplitterDistance = 233;
		this.splitContainer1.TabIndex = 7;
		this.dgvRows.AllowUserToAddRows = false;
		this.dgvRows.AllowUserToDeleteRows = false;
		this.dgvRows.AllowUserToResizeColumns = false;
		this.dgvRows.AllowUserToResizeRows = false;
		this.dgvRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvRows.Columns.AddRange(this.dgvRowsIDCol, this.dgvRowsNameCol);
		this.dgvRows.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvRows.Location = new System.Drawing.Point(0, 31);
		this.dgvRows.MinimumSize = new System.Drawing.Size(160, 0);
		this.dgvRows.MultiSelect = false;
		this.dgvRows.Name = "dgvRows";
		this.dgvRows.RowHeadersVisible = false;
		this.dgvRows.RowHeadersWidth = 51;
		this.dgvRows.Size = new System.Drawing.Size(233, 545);
		this.dgvRows.TabIndex = 1;
		this.dgvRows.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(dgvRows_CellContextMenuStripNeeded);
		this.dgvRows.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvRows_CellMouseDown);
		this.dgvRows.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(DgvRows_CellValidating);
		this.dgvRows.Scroll += new System.Windows.Forms.ScrollEventHandler(dgvRows_Scroll);
		this.dgvRows.SelectionChanged += new System.EventHandler(DgvRows_SelectionChanged);
		this.dgvRowsIDCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.dgvRowsIDCol.DataPropertyName = "ID";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
		this.dgvRowsIDCol.DefaultCellStyle = dataGridViewCellStyle2;
		this.dgvRowsIDCol.HeaderText = "行";
		this.dgvRowsIDCol.MinimumWidth = 6;
		this.dgvRowsIDCol.Name = "dgvRowsIDCol";
		this.dgvRowsIDCol.Width = 54;
		this.dgvRowsNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.dgvRowsNameCol.DataPropertyName = "Name";
		this.dgvRowsNameCol.HeaderText = "名";
		this.dgvRowsNameCol.MinimumWidth = 6;
		this.dgvRowsNameCol.Name = "dgvRowsNameCol";
		this.menuStrip3.BackColor = System.Drawing.Color.Transparent;
		this.menuStrip3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.menuStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.filter_Rows, this.button_FilterRows, this.button_ResetFilterRows });
		this.menuStrip3.Location = new System.Drawing.Point(0, 0);
		this.menuStrip3.Name = "menuStrip3";
		this.menuStrip3.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.menuStrip3.Size = new System.Drawing.Size(233, 31);
		this.menuStrip3.TabIndex = 2;
		this.menuStrip3.Text = "menuStrip3";
		this.filter_Rows.AutoToolTip = true;
		this.filter_Rows.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.filter_Rows.Margin = new System.Windows.Forms.Padding(2);
		this.filter_Rows.Name = "filter_Rows";
		this.filter_Rows.Size = new System.Drawing.Size(120, 23);
		this.filter_Rows.ToolTipText = resources.GetString("filter_Rows.ToolTipText");
		this.button_FilterRows.AutoToolTip = true;
		this.button_FilterRows.BackColor = System.Drawing.Color.DarkGray;
		this.button_FilterRows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_FilterRows.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.button_FilterRows.Margin = new System.Windows.Forms.Padding(2);
		this.button_FilterRows.Name = "button_FilterRows";
		this.button_FilterRows.Size = new System.Drawing.Size(50, 23);
		this.button_FilterRows.Text = "完成";
		this.button_FilterRows.ToolTipText = "Apply row view filter.";
		this.button_FilterRows.Click += new System.EventHandler(button_FilterRows_Click);
		this.button_ResetFilterRows.AutoToolTip = true;
		this.button_ResetFilterRows.BackColor = System.Drawing.Color.DarkGray;
		this.button_ResetFilterRows.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_ResetFilterRows.Margin = new System.Windows.Forms.Padding(2);
		this.button_ResetFilterRows.Name = "button_ResetFilterRows";
		this.button_ResetFilterRows.Size = new System.Drawing.Size(47, 23);
		this.button_ResetFilterRows.Text = "重设";
		this.button_ResetFilterRows.ToolTipText = "Reset row view.";
		this.button_ResetFilterRows.Click += new System.EventHandler(button_ResetFilterRows_Click);
		this.dgvCells.AllowUserToAddRows = false;
		this.dgvCells.AllowUserToDeleteRows = false;
		this.dgvCells.AllowUserToResizeColumns = false;
		this.dgvCells.AllowUserToResizeRows = false;
		this.dgvCells.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.dgvCells.Columns.AddRange(this.dgvCellsNameCol, this.dgvCellsEditorNameCol, this.dgvCellsValueCol, this.dgvCellsTypeCol);
		this.dgvCells.Dock = System.Windows.Forms.DockStyle.Fill;
		this.dgvCells.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
		this.dgvCells.Location = new System.Drawing.Point(0, 31);
		this.dgvCells.MinimumSize = new System.Drawing.Size(160, 0);
		this.dgvCells.MultiSelect = false;
		this.dgvCells.Name = "dgvCells";
		this.dgvCells.RowHeadersVisible = false;
		this.dgvCells.RowHeadersWidth = 51;
		this.dgvCells.Size = new System.Drawing.Size(483, 545);
		this.dgvCells.TabIndex = 2;
		this.dgvCells.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(dgvCells_CellContextMenuStripNeeded);
		this.dgvCells.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(DgvCells_CellFormatting);
		this.dgvCells.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(dgvCells_CellMouseDown);
		this.dgvCells.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(DgvCells_CellParsing);
		this.dgvCells.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(DgvCells_CellToolTipTextNeeded);
		this.dgvCells.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(DgvCells_CellValidating);
		this.dgvCells.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(DgvCells_DataBindingComplete);
		this.dgvCells.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(DgvCells_DataError);
		this.dgvCells.Scroll += new System.Windows.Forms.ScrollEventHandler(dgvCells_Scroll);
		this.dgvCells.SelectionChanged += new System.EventHandler(dgvCells_SelectionChanged);
		this.dgvCellsNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.dgvCellsNameCol.DataPropertyName = "Name";
		this.dgvCellsNameCol.HeaderText = "字段";
		this.dgvCellsNameCol.MinimumWidth = 60;
		this.dgvCellsNameCol.Name = "dgvCellsNameCol";
		this.dgvCellsNameCol.ReadOnly = true;
		this.dgvCellsNameCol.Width = 60;
		this.dgvCellsEditorNameCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.dgvCellsEditorNameCol.DataPropertyName = "EditorName";
		this.dgvCellsEditorNameCol.HeaderText = "字段";
		this.dgvCellsEditorNameCol.MinimumWidth = 60;
		this.dgvCellsEditorNameCol.Name = "dgvCellsEditorNameCol";
		this.dgvCellsEditorNameCol.ReadOnly = true;
		this.dgvCellsEditorNameCol.Width = 60;
		this.dgvCellsValueCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.dgvCellsValueCol.DataPropertyName = "Value";
		this.dgvCellsValueCol.HeaderText = "值";
		this.dgvCellsValueCol.MinimumWidth = 50;
		this.dgvCellsValueCol.Name = "dgvCellsValueCol";
		this.dgvCellsValueCol.Width = 59;
		this.dgvCellsTypeCol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.dgvCellsTypeCol.DataPropertyName = "Type";
		this.dgvCellsTypeCol.HeaderText = "Type";
		this.dgvCellsTypeCol.MinimumWidth = 30;
		this.dgvCellsTypeCol.Name = "dgvCellsTypeCol";
		this.dgvCellsTypeCol.ReadOnly = true;
		this.dgvCellsTypeCol.Width = 56;
		this.menuStrip4.BackColor = System.Drawing.Color.Transparent;
		this.menuStrip4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.menuStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.filter_Cells, this.button_FilterCells, this.button_ResetFilterCells });
		this.menuStrip4.Location = new System.Drawing.Point(0, 0);
		this.menuStrip4.Name = "menuStrip4";
		this.menuStrip4.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.menuStrip4.Size = new System.Drawing.Size(483, 31);
		this.menuStrip4.TabIndex = 3;
		this.menuStrip4.Text = "menuStrip4";
		this.filter_Cells.AutoToolTip = true;
		this.filter_Cells.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.filter_Cells.Margin = new System.Windows.Forms.Padding(2);
		this.filter_Cells.Name = "filter_Cells";
		this.filter_Cells.Size = new System.Drawing.Size(120, 23);
		this.filter_Cells.ToolTipText = resources.GetString("filter_Cells.ToolTipText");
		this.button_FilterCells.AutoToolTip = true;
		this.button_FilterCells.BackColor = System.Drawing.Color.DarkGray;
		this.button_FilterCells.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_FilterCells.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.button_FilterCells.Margin = new System.Windows.Forms.Padding(2);
		this.button_FilterCells.Name = "button_FilterCells";
		this.button_FilterCells.Size = new System.Drawing.Size(50, 23);
		this.button_FilterCells.Text = "完成";
		this.button_FilterCells.ToolTipText = "Apply field view filter.";
		this.button_FilterCells.Click += new System.EventHandler(button_FilterCells_Click);
		this.button_ResetFilterCells.AutoToolTip = true;
		this.button_ResetFilterCells.BackColor = System.Drawing.Color.DarkGray;
		this.button_ResetFilterCells.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.button_ResetFilterCells.Margin = new System.Windows.Forms.Padding(2);
		this.button_ResetFilterCells.Name = "button_ResetFilterCells";
		this.button_ResetFilterCells.Size = new System.Drawing.Size(47, 23);
		this.button_ResetFilterCells.Text = "重设";
		this.button_ResetFilterCells.ToolTipText = "Reset field view.";
		this.button_ResetFilterCells.Click += new System.EventHandler(button_ResetFilterCells_Click);
		this.statusStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
		this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
		this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.processLabel });
		this.statusStrip1.Location = new System.Drawing.Point(2, 602);
		this.statusStrip1.Name = "statusStrip1";
		this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
		this.statusStrip1.Size = new System.Drawing.Size(973, 22);
		this.statusStrip1.SizingGrip = false;
		this.statusStrip1.TabIndex = 9;
		this.statusStrip1.Text = "statusStrip1";
		this.processLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
		this.processLabel.Name = "processLabel";
		this.processLabel.Size = new System.Drawing.Size(103, 17);
		this.processLabel.Text = "No active process.";
		this.fbdExport.Description = "Choose the folder to export parambnds to";
		this.fbdExport.RootFolder = System.Environment.SpecialFolder.MyComputer;
		this.fieldContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.GotoReference1MenuItem, this.GotoReference2MenuItem, this.GotoReference3MenuItem, this.GotoReference4MenuItem, this.GotoReference5MenuItem, this.GotoReference6MenuItem });
		this.fieldContextMenu.Name = "fieldContextMenu";
		this.fieldContextMenu.Size = new System.Drawing.Size(160, 136);
		this.fieldContextMenu.Opening += new System.ComponentModel.CancelEventHandler(fieldContextMenu_Opening);
		this.GotoReference1MenuItem.Name = "GotoReference1MenuItem";
		this.GotoReference1MenuItem.Size = new System.Drawing.Size(159, 22);
		this.GotoReference1MenuItem.Text = "Reference 1 Text";
		this.GotoReference1MenuItem.Click += new System.EventHandler(GotoReference1MenuItem_Click);
		this.GotoReference2MenuItem.Name = "GotoReference2MenuItem";
		this.GotoReference2MenuItem.Size = new System.Drawing.Size(159, 22);
		this.GotoReference2MenuItem.Text = "Reference 2 Text";
		this.GotoReference2MenuItem.Click += new System.EventHandler(GotoReference2MenuItem_Click);
		this.GotoReference3MenuItem.Name = "GotoReference3MenuItem";
		this.GotoReference3MenuItem.Size = new System.Drawing.Size(159, 22);
		this.GotoReference3MenuItem.Text = "Reference 3 Text";
		this.GotoReference3MenuItem.Click += new System.EventHandler(GotoReference3MenuItem_Click);
		this.GotoReference4MenuItem.Name = "GotoReference4MenuItem";
		this.GotoReference4MenuItem.Size = new System.Drawing.Size(159, 22);
		this.GotoReference4MenuItem.Text = "Reference 4 Text";
		this.GotoReference4MenuItem.Click += new System.EventHandler(GotoReference4MenuItem_Click);
		this.GotoReference5MenuItem.Name = "GotoReference5MenuItem";
		this.GotoReference5MenuItem.Size = new System.Drawing.Size(159, 22);
		this.GotoReference5MenuItem.Text = "Reference 5 Text";
		this.GotoReference5MenuItem.Click += new System.EventHandler(GotoReference5MenuItem_Click);
		this.GotoReference6MenuItem.Name = "GotoReference6MenuItem";
		this.GotoReference6MenuItem.Size = new System.Drawing.Size(159, 22);
		this.GotoReference6MenuItem.Text = "Reference 6 Text";
		this.GotoReference6MenuItem.Click += new System.EventHandler(GotoReference6MenuItem_Click);
		this.rowContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.copyToParamMenuItem });
		this.rowContextMenu.Name = "rowContextMenu";
		this.rowContextMenu.Size = new System.Drawing.Size(154, 26);
		this.rowContextMenu.Opening += new System.ComponentModel.CancelEventHandler(rowContextMenu_Opening);
		this.copyToParamMenuItem.Name = "copyToParamMenuItem";
		this.copyToParamMenuItem.Size = new System.Drawing.Size(153, 22);
		this.copyToParamMenuItem.Text = "Copy to Param";
		this.copyToParamMenuItem.Click += new System.EventHandler(copyToParamMenuItem_Click);
		this.secondaryFilePath.Filter = "All files|*.*";
		this.BottomToolStripPanel.Location = new System.Drawing.Point(0, 0);
		this.BottomToolStripPanel.Name = "BottomToolStripPanel";
		this.BottomToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.BottomToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.BottomToolStripPanel.Size = new System.Drawing.Size(0, 0);
		this.TopToolStripPanel.Location = new System.Drawing.Point(0, 0);
		this.TopToolStripPanel.Name = "TopToolStripPanel";
		this.TopToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.TopToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.TopToolStripPanel.Size = new System.Drawing.Size(0, 0);
		this.RightToolStripPanel.Location = new System.Drawing.Point(0, 0);
		this.RightToolStripPanel.Name = "RightToolStripPanel";
		this.RightToolStripPanel.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.RightToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.RightToolStripPanel.Size = new System.Drawing.Size(0, 0);
		this.LeftToolStripPanel.Location = new Point(0, 0);
		this.LeftToolStripPanel.Name = "LeftToolStripPanel";
		this.LeftToolStripPanel.Orientation = Orientation.Horizontal;
		this.LeftToolStripPanel.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
		this.LeftToolStripPanel.Size = new System.Drawing.Size(0, 0);
		this.ContentPanel.Size = new System.Drawing.Size(150, 150);
		this.viewFilterSettingsToolStripMenuItem.Name = "viewFilterSettingsToolStripMenuItem";
		this.viewFilterSettingsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
		this.viewFilterSettingsToolStripMenuItem.Text = "View Filter Settings";
		this.viewFilterSettingsToolStripMenuItem.Click += new System.EventHandler(viewFilterSettingsToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.LightGray;
		base.ClientSize = new System.Drawing.Size(977, 626);
		base.Controls.Add(this.splitContainer2);
		base.Controls.Add(this.menuStrip1);
		base.Controls.Add(this.statusStrip1);
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Main";
		base.Padding = new System.Windows.Forms.Padding(2);
		this.Text = "Yapped <version>";
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(FormMain_FormClosing);
		base.Load += new System.EventHandler(Main_Load);
		this.menuStrip1.ResumeLayout(false);
		this.menuStrip1.PerformLayout();
		this.splitContainer2.Panel1.ResumeLayout(false);
		this.splitContainer2.Panel1.PerformLayout();
		this.splitContainer2.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.splitContainer2).EndInit();
		this.splitContainer2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvParams).EndInit();
		this.menuStrip2.ResumeLayout(false);
		this.menuStrip2.PerformLayout();
		this.splitContainer1.Panel1.ResumeLayout(false);
		this.splitContainer1.Panel1.PerformLayout();
		this.splitContainer1.Panel2.ResumeLayout(false);
		this.splitContainer1.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.splitContainer1).EndInit();
		this.splitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dgvRows).EndInit();
		this.menuStrip3.ResumeLayout(false);
		this.menuStrip3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.dgvCells).EndInit();
		this.menuStrip4.ResumeLayout(false);
		this.menuStrip4.PerformLayout();
		this.statusStrip1.ResumeLayout(false);
		this.statusStrip1.PerformLayout();
		this.fieldContextMenu.ResumeLayout(false);
		this.rowContextMenu.ResumeLayout(false);
		base.ResumeLayout(false);
		base.PerformLayout();
	}
}
