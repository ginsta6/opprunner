﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by coded UI test builder.
//      Version: 16.0.0.0
//
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------

namespace CodedUITestProject1
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text.RegularExpressions;
    using System.Windows.Input;
    using Microsoft.VisualStudio.TestTools.UITest.Extension;
    using Microsoft.VisualStudio.TestTools.UITesting;
    using Microsoft.VisualStudio.TestTools.UITesting.WpfControls;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Keyboard = Microsoft.VisualStudio.TestTools.UITesting.Keyboard;
    using Mouse = Microsoft.VisualStudio.TestTools.UITesting.Mouse;
    using MouseButtons = System.Windows.Forms.MouseButtons;
    
    
    [GeneratedCode("Coded UITest Builder", "16.0.31306.167")]
    public partial class UIMap
    {
        
        /// <summary>
        /// RecordedMethod1 - Use 'RecordedMethod1Params' to pass parameters into this method.
        /// </summary>
        public void RecordedMethod1()
        {
            #region Variable Declarations
            WpfEdit uINameInputEdit = this.UIRunnerWindow.UINameInputEdit;
            WpfText uIPinkmonsterText1 = this.UIRunnerWindow.UIPinkmonsterText.UIPinkmonsterText1;
            WpfButton uIJoinlobbyButton = this.UIRunnerWindow.UIJoinlobbyButton;
            WpfText uIName1Text1 = this.UIRunnerWindow.UIName1Text.UIName1Text1;
            WpfText uIPinkmonsterText = this.UIRunnerWindow.UIPinkmonsterText1.UIPinkmonsterText;
            #endregion

            uINameInputEdit.Text = "Name1";
            Assert.AreEqual(true, true);
            // Type 'Name1' in 'nameInput' text box
            //uINameInputEdit.Text = this.RecordedMethod1Params.UINameInputEditText;

            //// Click 'Pink monster' label
            //Mouse.Click(uIPinkmonsterText1, new Point(136, 32));

            //// Click 'Join lobby' button
            //Mouse.Click(uIJoinlobbyButton, new Point(301, 41));

            //// Click 'Name1' label
            //Mouse.Click(uIName1Text1, new Point(58, 65));

            //// Click 'Pink monster' label
            //Mouse.Click(uIPinkmonsterText, new Point(195, 43));
        }

        #region Properties
        public virtual RecordedMethod1Params RecordedMethod1Params
        {
            get
            {
                if ((this.mRecordedMethod1Params == null))
                {
                    this.mRecordedMethod1Params = new RecordedMethod1Params();
                }
                return this.mRecordedMethod1Params;
            }
        }
        
        public UIRunnerWindow UIRunnerWindow
        {
            get
            {
                if ((this.mUIRunnerWindow == null))
                {
                    this.mUIRunnerWindow = new UIRunnerWindow();
                }
                return this.mUIRunnerWindow;
            }
        }
        #endregion
        
        #region Fields
        private RecordedMethod1Params mRecordedMethod1Params;
        
        private UIRunnerWindow mUIRunnerWindow;
        #endregion
    }
    
    /// <summary>
    /// Parameters to be passed into 'RecordedMethod1'
    /// </summary>
    [GeneratedCode("Coded UITest Builder", "16.0.31306.167")]
    public class RecordedMethod1Params
    {
        
        #region Fields
        /// <summary>
        /// Type 'Name1' in 'nameInput' text box
        /// </summary>
        public string UINameInputEditText = "Name1";
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "16.0.31306.167")]
    public class UIRunnerWindow : WpfWindow
    {
        
        public UIRunnerWindow()
        {
            #region Search Criteria
            this.SearchProperties[WpfWindow.PropertyNames.Name] = "Runner";
            this.SearchProperties.Add(new PropertyExpression(WpfWindow.PropertyNames.ClassName, "HwndWrapper", PropertyExpressionOperator.Contains));
            this.WindowTitles.Add("Runner");
            #endregion
        }
        
        #region Properties
        public WpfEdit UINameInputEdit
        {
            get
            {
                if ((this.mUINameInputEdit == null))
                {
                    this.mUINameInputEdit = new WpfEdit(this);
                    #region Search Criteria
                    this.mUINameInputEdit.SearchProperties[WpfEdit.PropertyNames.AutomationId] = "nameInput";
                    this.mUINameInputEdit.WindowTitles.Add("Runner");
                    #endregion
                }
                return this.mUINameInputEdit;
            }
        }
        
        public UIPinkmonsterText UIPinkmonsterText
        {
            get
            {
                if ((this.mUIPinkmonsterText == null))
                {
                    this.mUIPinkmonsterText = new UIPinkmonsterText(this);
                }
                return this.mUIPinkmonsterText;
            }
        }
        
        public WpfButton UIJoinlobbyButton
        {
            get
            {
                if ((this.mUIJoinlobbyButton == null))
                {
                    this.mUIJoinlobbyButton = new WpfButton(this);
                    #region Search Criteria
                    this.mUIJoinlobbyButton.SearchProperties[WpfButton.PropertyNames.AutomationId] = "joinLobbyBtn";
                    this.mUIJoinlobbyButton.WindowTitles.Add("Runner");
                    #endregion
                }
                return this.mUIJoinlobbyButton;
            }
        }
        
        public UIName1Text UIName1Text
        {
            get
            {
                if ((this.mUIName1Text == null))
                {
                    this.mUIName1Text = new UIName1Text(this);
                }
                return this.mUIName1Text;
            }
        }
        
        public UIPinkmonsterText1 UIPinkmonsterText1
        {
            get
            {
                if ((this.mUIPinkmonsterText1 == null))
                {
                    this.mUIPinkmonsterText1 = new UIPinkmonsterText1(this);
                }
                return this.mUIPinkmonsterText1;
            }
        }
        #endregion
        
        #region Fields
        private WpfEdit mUINameInputEdit;
        
        private UIPinkmonsterText mUIPinkmonsterText;
        
        private WpfButton mUIJoinlobbyButton;
        
        private UIName1Text mUIName1Text;
        
        private UIPinkmonsterText1 mUIPinkmonsterText1;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "16.0.31306.167")]
    public class UIPinkmonsterText : WpfText
    {
        
        public UIPinkmonsterText(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfText.PropertyNames.AutomationId] = "CharacterTypeSelected";
            this.WindowTitles.Add("Runner");
            #endregion
        }
        
        #region Properties
        public WpfText UIPinkmonsterText1
        {
            get
            {
                if ((this.mUIPinkmonsterText1 == null))
                {
                    this.mUIPinkmonsterText1 = new WpfText(this);
                    #region Search Criteria
                    this.mUIPinkmonsterText1.SearchProperties[WpfText.PropertyNames.Name] = "Pink monster ";
                    this.mUIPinkmonsterText1.SearchConfigurations.Add(SearchConfiguration.DisambiguateChild);
                    this.mUIPinkmonsterText1.WindowTitles.Add("Runner");
                    #endregion
                }
                return this.mUIPinkmonsterText1;
            }
        }
        #endregion
        
        #region Fields
        private WpfText mUIPinkmonsterText1;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "16.0.31306.167")]
    public class UIName1Text : WpfText
    {
        
        public UIName1Text(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfText.PropertyNames.AutomationId] = "players";
            this.WindowTitles.Add("Runner");
            #endregion
        }
        
        #region Properties
        public WpfText UIName1Text1
        {
            get
            {
                if ((this.mUIName1Text1 == null))
                {
                    this.mUIName1Text1 = new WpfText(this);
                    #region Search Criteria
                    this.mUIName1Text1.SearchProperties[WpfText.PropertyNames.Name] = "Name1\n";
                    this.mUIName1Text1.SearchConfigurations.Add(SearchConfiguration.DisambiguateChild);
                    this.mUIName1Text1.WindowTitles.Add("Runner");
                    #endregion
                }
                return this.mUIName1Text1;
            }
        }
        #endregion
        
        #region Fields
        private WpfText mUIName1Text1;
        #endregion
    }
    
    [GeneratedCode("Coded UITest Builder", "16.0.31306.167")]
    public class UIPinkmonsterText1 : WpfText
    {
        
        public UIPinkmonsterText1(UITestControl searchLimitContainer) : 
                base(searchLimitContainer)
        {
            #region Search Criteria
            this.SearchProperties[WpfText.PropertyNames.AutomationId] = "CharacterTypeSelectedLobby";
            this.WindowTitles.Add("Runner");
            #endregion
        }
        
        #region Properties
        public WpfText UIPinkmonsterText
        {
            get
            {
                if ((this.mUIPinkmonsterText == null))
                {
                    this.mUIPinkmonsterText = new WpfText(this);
                    #region Search Criteria
                    this.mUIPinkmonsterText.SearchProperties[WpfText.PropertyNames.Name] = "Pink monster ";
                    this.mUIPinkmonsterText.SearchConfigurations.Add(SearchConfiguration.DisambiguateChild);
                    this.mUIPinkmonsterText.WindowTitles.Add("Runner");
                    #endregion
                }
                return this.mUIPinkmonsterText;
            }
        }
        #endregion
        
        #region Fields
        private WpfText mUIPinkmonsterText;
        #endregion
    }
}
