﻿using System.Windows.Forms;

namespace GitUIPluginInterfaces
{
    public class PasswordSetting : ISetting
    {
        public PasswordSetting(string aName, string aDefaultValue)
            : this(aName, aName, aDefaultValue)
        {
        }

        public PasswordSetting(string aName, string aCaption, string aDefaultValue)
        {
            Name = aName;
            Caption = aCaption;
            DefaultValue = aDefaultValue;
        }

        public string Name { get; private set; }
        public string Caption { get; private set; }
        public string DefaultValue { get; set; }

        public ISettingControlBinding CreateControlBinding()
        {
            return new TextBoxBinding(this);
    }

        private class TextBoxBinding : SettingControlBinding<PasswordSetting, TextBox>
        {
            public TextBoxBinding(PasswordSetting aSetting)
                : base(aSetting)
            { }

            public override TextBox CreateControl()
            {
                return new TextBox {PasswordChar = '\u25CF'};
            }

            public override void LoadSetting(ISettingsSource settings, bool areSettingsEffective, TextBox control)
            {
                string settingVal;
                if (areSettingsEffective)
                {
                    settingVal = Setting.ValueOrDefault(settings);
                }
                else
                {
                    settingVal = Setting[settings];
                }

                control.Text = settingVal;
            }

            public override void SaveSetting(ISettingsSource settings, TextBox control)
            {
                Setting[settings] = control.Text;
            }
        }

        public string this[ISettingsSource settings]
        {
            get
            {
                return settings.GetString(Name, null);
            }

            set
            {
                settings.SetString(Name, value);
            }
        }

        public string ValueOrDefault(ISettingsSource settings)
        {
            return this[settings] ?? DefaultValue;
        }
    }
}