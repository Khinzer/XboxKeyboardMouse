﻿using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace XboxKeyboardMouse.Forms {
    public partial class Options : Controls.FormRWE {

        public Options() {
            InitializeComponent();

            this.MouseDown += FormMouseMove;

            materialTabSelector1.MouseMove += MaterialTabSelector1_MouseMove;
            btnExit.MouseMove              += MaterialTabSelector1_MouseMove;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            // Setup preset creation
            file_CreatePreset_Button.SetFontColor = true;
            file_CreatePreset_Button.SetFontDisabledColor = true;
            file_CreatePreset_Button.FontColorDisabled = preset_Color_Exists;
            file_CheckIfExists(null, null);

            // Display the current active file name
            file_Active.Text = "Active Preset: " + Program.ActiveConfig.Name;
        }

        

        public override void SetStatusColor(Color c) {
            btnExit.BackColor = c;
        }

    // --> REGIONS

        #region Events
        // -----------

        private void MaterialTabSelector1_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                FormMouseMove(this, e);
            }
        }

        private void ExitForm(object sender, EventArgs e) {
            this.Hide();
        }

        private void Options_Load(object sender, EventArgs e) {
            ResetXboxInputButtons();
            RefreshConfigList();
            ApplyInputEvents();

            materialTabControl1.Controls.Remove(tabSettings);
            materialTabControl1.Controls.Remove(tabKeyboard);
            materialTabControl1.Controls.Remove(tabMouse);
            materialTabControl1.Controls.Remove(tabPage3);
            materialTabSelector1.Invalidate();
        }
        
        // -----------
        #endregion

        #region Xbox Input Editor
        //
        // Summary:
        //     Defines values that specify the buttons on a mouse device.
        public enum MouseButton {
            //
            // Summary:
            //     Nothing
            None = -1,

            //
            // Summary:
            //     The left mouse button.
            Left = 0,
            //
            // Summary:
            //     The middle mouse button.
            Middle = 1,
            //
            // Summary:
            //     The right mouse button.
            Right = 2,
            //
            // Summary:
            //     The first extended mouse button.
            XButton1 = 3,
            //
            // Summary:
            //     The second extended mouse button.
            XButton2 = 4
        }

        private void ResetXboxInputButtons() {
            // Keyboard
            xbo_k_A.Text = "";
            xbo_k_B.Text = "";
            xbo_k_X.Text = "";
            xbo_k_Y.Text = "";

            xbo_k_LeftSholder.Text = "";
            xbo_k_DpadLeft.Text = "";
            xbo_k_LeftStick.Text = "";

            xbo_k_RightSholder.Text = "";
            xbo_k_DpadRight.Text = "";
            xbo_k_RightStick.Text = "";

            xbo_k_DpadUp.Text = "";
            xbo_k_DpadDown.Text = "";

            xbo_k_Start.Text = "";
            xbo_k_Back.Text = "";
            xbo_k_Guide.Text = "";

            xbo_k_TLeft.Text = "";
            xbo_k_TRight.Text = "";

            xbo_k_joy_l_up.Text = "";
            xbo_k_joy_l_down.Text = "";
            xbo_k_joy_l_left.Text = "";
            xbo_k_joy_l_right.Text = "";

            xbo_k_joy_r_up.Text = "";
            xbo_k_joy_r_down.Text = "";
            xbo_k_joy_r_left.Text = "";
            xbo_k_joy_r_right.Text = "";

            /*/ Mouse
            xbo_m_A.Text = "";
            xbo_m_B.Text = "";
            xbo_m_X.Text = "";
            xbo_m_Y.Text = "";

            xbo_m_Start.Text = "";
            xbo_m_Back.Text = "";
            xbo_m_Guide.Text = "";

            xbo_m_RS.Text = "";
            xbo_m_LS.Text = "";

            xbo_m_SR.Text = "";
            xbo_m_SL.Text = "";

            xbo_m_DpadUp.Text = "";
            xbo_m_DpadDown.Text = "";
            xbo_m_DpadLeft.Text = "";
            xbo_m_DpadRight.Text = "";

            xbo_m_TLeft.Text = "";
            xbo_m_TRight.Text = "";
            //*/
        }

        private void LoadXboxInputButtons() {
            // Keyboard
            xbo_k_A.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_A).ToString();
            xbo_k_B.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_B).ToString();
            xbo_k_X.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_X).ToString();
            xbo_k_Y.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Y).ToString();

            xbo_k_LeftSholder.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_LeftBumper).ToString();
            xbo_k_DpadLeft.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_DPAD_Left).ToString();
            xbo_k_LeftStick.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Sticks_Left).ToString();

            xbo_k_RightSholder.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_RightBumper).ToString();
            xbo_k_DpadRight.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_DPAD_Right).ToString();
            xbo_k_RightStick.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Sticks_Right).ToString();

            xbo_k_DpadUp.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_DPAD_Up).ToString();
            xbo_k_DpadDown.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_DPAD_Down).ToString();

            xbo_k_Start.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Start).ToString();
            xbo_k_Back.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Back).ToString();
            xbo_k_Guide.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Guide).ToString();

            xbo_k_TLeft.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Trigger_Left).ToString();
            xbo_k_TRight.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Xbox_Trigger_Right).ToString();

            xbo_k_joy_l_up.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_L_Up).ToString(); ;
            xbo_k_joy_l_down.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_L_Down).ToString(); ;
            xbo_k_joy_l_left.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_L_Left).ToString(); ;
            xbo_k_joy_l_right.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_L_Right).ToString(); ;

            xbo_k_joy_r_up.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_R_Up).ToString(); ;
            xbo_k_joy_r_down.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_R_Down).ToString(); ;
            xbo_k_joy_r_left.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_R_Left).ToString(); ;
            xbo_k_joy_r_right.Text = ((System.Windows.Input.Key)cfg.Controls_KB_Sticks_AXIS_R_Right).ToString(); ;

            /*/ Mouse
            xbo_m_A.Text = ((MouseButton)cfg.Controls_M_Xbox_A).ToString();
            xbo_m_B.Text = ((MouseButton)cfg.Controls_M_Xbox_B).ToString();
            xbo_m_X.Text = ((MouseButton)cfg.Controls_M_Xbox_X).ToString();
            xbo_m_Y.Text = ((MouseButton)cfg.Controls_M_Xbox_Y).ToString();

            xbo_m_Start.Text = ((MouseButton)cfg.Controls_M_Xbox_Start).ToString();
            xbo_m_Back.Text = ((MouseButton)cfg.Controls_M_Xbox_Back).ToString();
            xbo_m_Guide.Text = ((MouseButton)cfg.Controls_M_Xbox_Guide).ToString();

            xbo_m_RS.Text = ((MouseButton)cfg.Controls_M_Xbox_Sticks_Right).ToString();
            xbo_m_LS.Text = ((MouseButton)cfg.Controls_M_Xbox_Sticks_Left).ToString();

            xbo_m_SR.Text = ((MouseButton)cfg.Controls_M_Xbox_RightBumper).ToString();
            xbo_m_SL.Text = ((MouseButton)cfg.Controls_M_Xbox_LeftBumper).ToString();

            xbo_m_DpadUp.Text = ((MouseButton)cfg.Controls_M_Xbox_DPAD_Up).ToString();
            xbo_m_DpadDown.Text = ((MouseButton)cfg.Controls_M_Xbox_DPAD_Down).ToString();
            xbo_m_DpadLeft.Text = ((MouseButton)cfg.Controls_M_Xbox_DPAD_Left).ToString();
            xbo_m_DpadRight.Text = ((MouseButton)cfg.Controls_M_Xbox_DPAD_Right).ToString();

            xbo_m_TLeft.Text = ((MouseButton)cfg.Controls_M_Xbox_Trigger_Left).ToString();
            xbo_m_TRight.Text = ((MouseButton)cfg.Controls_M_Xbox_Trigger_Right).ToString();
            //*/
        }

        public void ApplyInputEvents() {
            foreach (Control ctrl in editor_InputKeyboard.Controls) {
                if (ctrl is Label) {
                    ctrl.MouseHover += XBO_Input_OnEnter;
                    ctrl.MouseLeave += XBO_Input_OnLeave;
                    ctrl.MouseDoubleClick += XBO_Input_SelectKey;
                }
            }

            xbo_k_TLeft.MouseHover        += XBO_Input_OnEnter;
            xbo_k_TLeft.MouseLeave        += XBO_Input_OnLeaveTrig;
            xbo_k_TLeft.MouseDoubleClick  += XBO_Input_SelectKey;
            xbo_k_TRight.MouseHover       += XBO_Input_OnEnter;
            xbo_k_TRight.MouseLeave       += XBO_Input_OnLeaveTrig;
            xbo_k_TRight.MouseDoubleClick += XBO_Input_SelectKey;
        }

        #region Input Handlers
        private void XBO_Input_SelectKey(object sender, System.Windows.Forms.MouseEventArgs e) {
            var lbl = (Label)sender;

            var bEscape = Hooks.LowLevelKeyboardHook.LockEscape;
            Hooks.LowLevelKeyboardHook.LockEscape = false; {
                SelectKey k = new SelectKey(cfg, (string)lbl.Tag);
                k.ShowDialog();
            } Hooks.LowLevelKeyboardHook.LockEscape = bEscape;

            LoadXboxInputButtons();
        }

        private void XBO_Input_OnLeave(object sender, EventArgs e) {
            var lbl = (Label)sender;
            var lblTag = (string)lbl.Tag;
            if (lblTag.StartsWith("JR") || lblTag.StartsWith("JL"))
                lbl.ForeColor = Color.Black;
            else lbl.ForeColor = Color.White;
        }

        private void XBO_Input_OnLeaveTrig(object sender, EventArgs e) {
            var lbl = (Label)sender;
            lbl.ForeColor = Color.Black;
        }

        private void XBO_Input_OnEnter(object sender, EventArgs e) {
            var lbl = (Label)sender;
            lbl.ForeColor = Color.FromArgb(93, 194, 30);
        }
        #endregion

        #endregion

        #region Configuration
        // ------------------
        Config.Data cfg = new Config.Data();
        string originalName = "";
        string SelectedProfile = "";

        private void RefreshConfigList(string Selected = "") {
            lbPresets.Items.Clear();

            var d = Directory.GetFiles("profiles", "*.ini", SearchOption.TopDirectoryOnly);
            foreach (var f in d) {
                var presetFile = Path.GetFileNameWithoutExtension(f);
                var item = new ListViewItem(presetFile);
                lbPresets.Items.Add(item);
            }
        }

        public string GetSelectedListProfile() {
            return lbPresets.SelectedItems.Count > 0 ? lbPresets.SelectedItems[0].Text : null;
        }

        public string GetSelectedListProfilePath() {
            return Path.Combine("profiles", GetSelectedListProfile() + ".ini");
        }

        public string GetSelectedProfile() {
            return SelectedProfile;
        }

        public string GetSelectedProfilePath() {
            return Path.Combine("profiles", GetSelectedProfile() + ".ini");
        }

        #region Preset Creation
        Color preset_Color_Default = Color.FromArgb(0xFF, 0x21, 0x21, 0x21);
        Color preset_Color_Exists  = ((int)Primary.Red800).ToColor();
        Color preset_Color_Created = ((int)Primary.Green500).ToColor();

        // Check if a preset exists
        private bool presetNameExists() {
            return File.Exists(Path.Combine("profiles", file_CreatePreset_Text.Text + ".ini"));
        }

        // Refresh the listing
        private void file_RefreshList_Click(object sender, EventArgs e) {
            RefreshConfigList();
        }

        // Check if preset exists
        private void file_CheckIfExists(object sender, System.Windows.Forms.KeyEventArgs e) {
            if (presetNameExists()) {
                file_CreatePreset_Button.Enabled = false;
            } else {
                file_CreatePreset_Button.Enabled = true;
                file_CreatePreset_Button.FontColor = preset_Color_Default;
            }
        }

        // Create the preset
        private void file_CreatePreset_Button_Click(object sender, EventArgs e) {
            var presetString = file_CreatePreset_Text.Text;
            var filePath = Path.Combine("profiles", presetString + ".ini");
            
            if (presetNameExists()) {
                file_CreatePreset_Button.Enabled = false;
                MessageBox.Show("Preset already exists (" + presetString + ".ini)");
                return;
            }

            // Create new config with default values
            Config.Data d = new Config.Data();
            d.Name = presetString;

            Config.Data.Save(presetString + ".ini", d);

            // Saved
            file_CreatePreset_Button.FontColor = preset_Color_Created;
            
            MessageBox.Show("Preset created in file (" + presetString + ".ini)");
            RefreshConfigList();
        }
        #endregion

        // ------------------
        #endregion

        #region Tabs
        // -------------------

        #region File
        // ---------

        bool TabsAdded = false;
        private void file_LoadPreset_Click(object sender, EventArgs e) {
            if (!File.Exists(GetSelectedListProfilePath())) {
                MessageBox.Show("The selected profile: " + GetSelectedProfile() +
                    " no longer exists...\nFile: " + GetSelectedProfilePath());
                RefreshConfigList();

                return;
            }

            // Load the config
            cfg = Config.Data.Load(GetSelectedListProfile() + ".ini");

            SelectedProfile = GetSelectedListProfile();

            // Load the controls onto the labels
            LoadXboxInputButtons();

            // Load the values for the options
            preset_Name.Text = cfg.Name;
            mouseInvertAxisX.Checked = cfg.Mouse_Invert_X;
            mouseInvertAxisY.Checked = cfg.Mouse_Invert_Y;

            // Check if the mouse setting is valid
            if (cfg.Mouse_Eng_Type > TranslateMouse.MaxMouseMode || cfg.Mouse_Eng_Type < 0) {
                MessageBox.Show("Invalid mouse engine selected -> Reset to default!");
                cfg.Mouse_Eng_Type = 0;
                Config.Data.Save(GetSelectedProfile() + ".ini", cfg);
            }

            mouseEngineList.SelectedIndex = cfg.Mouse_Eng_Type;

            // Ensure tickrate is not 0
            if (cfg.Mouse_TickRate == 0) {
                cfg.Mouse_TickRate = 40;
                Config.Data.Save(GetSelectedProfile() + ".ini", cfg);
            }

            mouse_TickRate.Text = "" + cfg.Mouse_TickRate;

            // Ensure that there is a detach key
            var k1 = (System.Windows.Input.Key)cfg.Controls_KB_Detach_MOD;
            var k2 = (System.Windows.Input.Key)cfg.Controls_KB_Detach_KEY;
            var no = System.Windows.Input.Key.None;

            if (k1 == no && k2 == no) {
                MessageBox.Show("You cant disable the detach key, reset to default (LeftAlt + C)!");

                cfg.Controls_KB_Detach_MOD = (int)System.Windows.Input.Key.LeftAlt;
                cfg.Controls_KB_Detach_KEY = (int)System.Windows.Input.Key.C;

                Config.Data.Save(GetSelectedProfile() + ".ini", cfg);
            } else {
                detachKeyCheckup(true);
            }

            // Load detach key 
            string kbMod = ((System.Windows.Input.Key)cfg.Controls_KB_Detach_MOD).ToString();
            kbMod = (kbMod == "None" ? "" : $"{kbMod} +");

            string kbKey = ((System.Windows.Input.Key)cfg.Controls_KB_Detach_KEY).ToString();
            settings_DetachKey.Text = $"On/Off Key: {kbMod} {kbKey}";

            // Display our current file and active files
            file_Editing.Text = "Editing: " + GetSelectedProfile();
            file_Active.Text  = "Active Preset: " + Program.ActiveConfig.Name;
            
            editor_InputKeyboard.Enabled = true;

            originalName = cfg.Name;

            // Load our application settings
            settings_LockEscape.Checked = cfg.Application_LockEscape;
            settings_ShowCursor.Checked = cfg.Application_ShowCursor;

            // Load our mouse engine
            LoadMouseEngineSettings();

            // Setup our sticks
            comboBox1.SelectedIndex = (cfg.Mouse_Is_RightStick ? 1 : 0);

            // Enable the tabs
            tabSettings.Enabled = true;
            tabKeyboard.Enabled = true;
            tabMouse.Enabled = true;

            // Add the tabs if not already added
            if (!TabsAdded) {
                TabsAdded = true;

                // Add the tabs as controls
                materialTabControl1.Controls.Add(tabSettings);
                materialTabControl1.Controls.Add(tabKeyboard);
                materialTabControl1.Controls.Add(tabMouse);
                materialTabControl1.Controls.Add(tabPage3);
            }
        }

        private void file_SetAsActive_Click(object sender, EventArgs e) {
            if (!File.Exists(GetSelectedListProfilePath())) {
                MessageBox.Show("The selected profile: " + GetSelectedProfile() +
                    " no longer exists...\nFile: " + GetSelectedProfilePath());
                RefreshConfigList();

                return;
            }

            var prof = GetSelectedProfile();
            Program.SetActiveConfig(prof + ".ini");

            file_Active.Text  = "Active Preset: " + Program.ActiveConfig.Name;
        }

        private void file_DeletePreset_Click(object sender, EventArgs e) {
            var res = MessageBox.Show(
                "Are you sure you want to delete: " + GetSelectedListProfile() + "\nFile: " + GetSelectedListProfilePath(),
                "Are you sure?",
                MessageBoxButtons.OKCancel
            );

            if (res != DialogResult.OK)
                return;

            if (!File.Exists(GetSelectedListProfilePath())) {
                MessageBox.Show("The selected profile: " + GetSelectedListProfile() +
                    " no longer exists...\nFile: " + GetSelectedListProfilePath());
                return;
            }

            if (GetSelectedListProfile().Trim() == "default.ini") {
                // Remake the default ini
                Config.Data d = new Config.Data();
                Config.Data.Save("profiles/default.ini", d);
            }

            if (Program.ActiveConfig.Name.Trim() == GetSelectedListProfilePath().Trim()) {
                Program.SetActiveConfig("default.ini");
            }
            
            file_Editing.Text = GetSelectedProfile();
            file_Editing.Enabled = true;
            file_Active.Text = Program.ActiveConfig.Name;

            File.Delete(GetSelectedListProfilePath());
            RefreshConfigList();
        }

        // ---------
        #endregion

        #region Settings
        // -------------

        private void settings_LockEscape_CheckedChanged(object sender, EventArgs e) {
            cfg.Application_LockEscape = settings_LockEscape.Checked;
        }

        private void settings_ShowCursor_CheckedChanged(object sender, EventArgs e) {
            //Program.HideCursor = !settings_ShowCursor.Checked;
            cfg.Application_ShowCursor = settings_ShowCursor.Checked;
        }

        private void settings_DetachKey_Click(object sender, EventArgs e) {
            // Get current modifers
            Models.MSelectKey_Storage storage = new Models.MSelectKey_Storage() {
                Cancel = false,
                inputKey = cfg.Controls_KB_Detach_KEY,
                inputMod = cfg.Controls_KB_Detach_MOD
            };

            var bEscape = Hooks.LowLevelKeyboardHook.LockEscape;
            Hooks.LowLevelKeyboardHook.LockEscape = false;
            {
                var frm = new SelectKey_Modifier(storage, true);
                frm.ShowDialog();
            }
            Hooks.LowLevelKeyboardHook.LockEscape = bEscape;

            if (storage.Cancel) goto CheckReturn;

            cfg.Controls_KB_Detach_KEY = storage.inputKey;
            cfg.Controls_KB_Detach_MOD = storage.inputMod;

        CheckReturn:
            var k1 = (System.Windows.Input.Key)cfg.Controls_KB_Detach_MOD;
            var k2 = (System.Windows.Input.Key)cfg.Controls_KB_Detach_KEY;
            var no = System.Windows.Input.Key.None;

            if (k1 == no && k2 == no) {
                MessageBox.Show("You cant disable the detach key, reset to default (LeftAlt + C)!");

                cfg.Controls_KB_Detach_MOD = (int)System.Windows.Input.Key.LeftAlt;
                cfg.Controls_KB_Detach_KEY = (int)System.Windows.Input.Key.C;
            }

            detachKeyCheckup();

            string kbMod = ((System.Windows.Input.Key)cfg.Controls_KB_Detach_MOD).ToString();
            kbMod = (kbMod == "None" ? "" : $"{kbMod} +");

            string kbKey = ((System.Windows.Input.Key)cfg.Controls_KB_Detach_KEY).ToString();
            settings_DetachKey.Text = $"On/Off Key: {kbMod} {kbKey}";
        }

        private void settings_SaveAllChanges_Click(object sender, EventArgs e) {
            // The dreaded save button ;(

            // Ignore name for now, that will
            // require extra work

            // Mouse settings
            cfg.Mouse_Invert_X = mouseInvertAxisX.Checked;
            cfg.Mouse_Invert_Y = mouseInvertAxisY.Checked;

            // TODO: Mouse Engine
            //cfg.Mouse_Sensitivity_X = (double)mouseXSense.Value;
            //cfg.Mouse_Sensitivity_Y = (double)mouseYSense.Value;
            //cfg.Mouse_Eng_Type = (int)mouseMouseEngine.SelectedIndex;
            //cfg.Mouse_FinalMod = (double)mouseMouseModifier.Value;

            if (cfg.Name.Trim() != preset_Name.Text.Trim()) {
                cfg.Name = preset_Name.Text.Trim();
                File.Delete(Path.Combine("profiles", GetSelectedProfile() + ".ini"));
            }

            // Saved
            Config.Data.Save(cfg.Name + ".ini", cfg);

            MessageBox.Show(
                "The selected profile: " + cfg.Name +
                " has been saved...\nFile: " + $"profiles/{cfg.Name}.ini");

            if (Program.ActiveConfig.Name == originalName) {
                Program.ActiveConfig = cfg;
                Program.ReloadActiveConfig();
            }


            // Refresh list
            RefreshConfigList();
        }

        private void settings_ChangePresetName_Click(object sender, EventArgs e) {
            var name = cfg.Name.Trim();

            if (cfg.Name.Trim() != preset_Name.Text.Trim()) {
                cfg.Name = preset_Name.Text.Trim();
                File.Delete(Path.Combine("profiles", GetSelectedProfile() + ".ini"));
            }

            // Saved
            Config.Data.Save(cfg.Name + ".ini", cfg);

            // Check if cfg is default
            if (name == "default") {
                // Remake the default ini
                Config.Data d = new Config.Data();
                Config.Data.Save("profiles/default.ini", d);
            }

            // Refresh listings
            RefreshConfigList();
        }
        
        // -------------
        #endregion

        #region Mouse
        // ----------

        private void mouseEngineList_SelectedIndexChanged(object sender, EventArgs e) {
            var index = mouseEngineList.SelectedIndex;

            if (index > TranslateMouse.MaxMouseMode) {
                MessageBox.Show("Invalid selected mouse engine");
                index = 0;
            }

            cfg.Mouse_Eng_Type = index;

            // Load our mouse engine settings
            LoadMouseEngineSettings();
        }

        private void mouseInvertAxisX_CheckedChanged(object sender, EventArgs e) {
            cfg.Mouse_Invert_X = mouseInvertAxisX.Checked;
        }

        private void mouseInvertAxisY_CheckedChanged(object sender, EventArgs e) {
            cfg.Mouse_Invert_Y = mouseInvertAxisY.Checked;
        }

        private void mouse_TickRate_TextChanged(object sender, EventArgs e) {
            string strTick = mouse_TickRate.Text;

            int tickRate = 0;
            if (! int.TryParse(strTick, out tickRate)) {
                mouse_TickInvalid.Visible = true;
                return;
            }

            // Min is 1 MS!
            if (tickRate <= 0) 
                tickRate = 1;
            

            mouse_TickInvalid.Visible = false;
            cfg.Mouse_TickRate = tickRate;
        }

        private void mouse_TickRate_Reset_Click(object sender, EventArgs e) {
            cfg.Mouse_TickRate = 16;
            mouse_TickRate.Text = "" + 16;
        }

        private void mouseSelectStick_SelectedIndexChanged(object sender, EventArgs e) {
            cfg.Mouse_Is_RightStick = comboBox1.SelectedIndex == 1;
        }

        MouseSettings.MouseEngineSettings mouseEnginePanel;
        private void LoadMouseEngineSettings() {
            if (mouseEnginePanel != null)  
                if (mouseEngineContainer.Controls.Contains(mouseEnginePanel))
                    mouseEngineContainer.Controls.Remove  (mouseEnginePanel);

            // Get our config index
            var index = cfg.Mouse_Eng_Type;
            MouseSettings.MouseEngineSettings engine = null;
            
            // By default we want to set engine as GenericControls
            if (index < TranslateMouse.MaxMouseMode) 
                engine = new MouseSettings.GenericControls(cfg);
            
            if (engine == null) 
                engine = new MouseSettings.NoControls();
            

            mouseEnginePanel    = engine;
            mouseEngineContainer.Controls.Add(engine);
            engine.Dock         = DockStyle.Fill;
        }

        // ----------
        #endregion

        #region Keyboard
        // -------------

        private void detachKeyCheckup(bool save = false) {
            var k1 = (System.Windows.Input.Key)cfg.Controls_KB_Detach_MOD;
            var k2 = (System.Windows.Input.Key)cfg.Controls_KB_Detach_KEY;

            if (k2 == System.Windows.Input.Key.None & k1 != System.Windows.Input.Key.None) {
                k2 = k1;
                k1 = System.Windows.Input.Key.None;
            }

            cfg.Controls_KB_Detach_MOD = (int)k1;
            cfg.Controls_KB_Detach_KEY = (int)k2;

            if (save)
                Config.Data.Save(GetSelectedProfile() + ".ini", cfg);
        }

        // -------------
        #endregion

        // -------------------
        #endregion

        // <-- REGIONS

    }
}
