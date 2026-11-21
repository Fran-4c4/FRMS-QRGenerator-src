#nullable enable

namespace FRMS.QRGenerator;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            imgQR.Image?.Dispose();
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblInstructions = new Label();
        txtCode = new TextBox();
        lblPayloadSize = new Label();
        grpOptions = new GroupBox();
        btnBackgroundColor = new Button();
        lblBackgroundColor = new Label();
        btnForegroundColor = new Button();
        lblForegroundColor = new Label();
        numModuleSize = new NumericUpDown();
        lblModuleSize = new Label();
        cmbErrorCorrection = new ComboBox();
        lblErrorCorrection = new Label();
        btnGenerate = new Button();
        btnSaveAs = new Button();
        btnCopy = new Button();
        imgQR = new PictureBox();
        lblStatus = new Label();
        grpOptions.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numModuleSize).BeginInit();
        ((System.ComponentModel.ISupportInitialize)imgQR).BeginInit();
        SuspendLayout();
        // 
        // lblInstructions
        // 
        lblInstructions.AutoSize = true;
        lblInstructions.Location = new Point(12, 12);
        lblInstructions.Name = "lblInstructions";
        lblInstructions.Size = new Size(241, 20);
        lblInstructions.TabIndex = 0;
        lblInstructions.Text = "Texto que se incluirá en el código QR:";
        // 
        // txtCode
        // 
        txtCode.AcceptsReturn = true;
        txtCode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtCode.Location = new Point(12, 38);
        txtCode.Multiline = true;
        txtCode.Name = "txtCode";
        txtCode.ScrollBars = ScrollBars.Vertical;
        txtCode.Size = new Size(896, 88);
        txtCode.TabIndex = 1;
        txtCode.TextChanged += txtCode_TextChanged;
        // 
        // lblPayloadSize
        // 
        lblPayloadSize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblPayloadSize.Location = new Point(632, 129);
        lblPayloadSize.Name = "lblPayloadSize";
        lblPayloadSize.Size = new Size(276, 24);
        lblPayloadSize.TabIndex = 2;
        lblPayloadSize.TextAlign = ContentAlignment.MiddleRight;
        // 
        // grpOptions
        // 
        grpOptions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpOptions.Controls.Add(btnBackgroundColor);
        grpOptions.Controls.Add(lblBackgroundColor);
        grpOptions.Controls.Add(btnForegroundColor);
        grpOptions.Controls.Add(lblForegroundColor);
        grpOptions.Controls.Add(numModuleSize);
        grpOptions.Controls.Add(lblModuleSize);
        grpOptions.Controls.Add(cmbErrorCorrection);
        grpOptions.Controls.Add(lblErrorCorrection);
        grpOptions.Location = new Point(12, 156);
        grpOptions.Name = "grpOptions";
        grpOptions.Size = new Size(896, 82);
        grpOptions.TabIndex = 3;
        grpOptions.TabStop = false;
        grpOptions.Text = "Opciones";
        // 
        // btnBackgroundColor
        // 
        btnBackgroundColor.Location = new Point(758, 36);
        btnBackgroundColor.Name = "btnBackgroundColor";
        btnBackgroundColor.Size = new Size(119, 30);
        btnBackgroundColor.TabIndex = 7;
        btnBackgroundColor.UseVisualStyleBackColor = false;
        btnBackgroundColor.Click += btnBackgroundColor_Click;
        // 
        // lblBackgroundColor
        // 
        lblBackgroundColor.AutoSize = true;
        lblBackgroundColor.Location = new Point(702, 14);
        lblBackgroundColor.Name = "lblBackgroundColor";
        lblBackgroundColor.Size = new Size(48, 20);
        lblBackgroundColor.TabIndex = 6;
        lblBackgroundColor.Text = "Fondo";
        // 
        // btnForegroundColor
        // 
        btnForegroundColor.Location = new Point(574, 36);
        btnForegroundColor.Name = "btnForegroundColor";
        btnForegroundColor.Size = new Size(119, 30);
        btnForegroundColor.TabIndex = 5;
        btnForegroundColor.UseVisualStyleBackColor = false;
        btnForegroundColor.Click += btnForegroundColor_Click;
        // 
        // lblForegroundColor
        // 
        lblForegroundColor.AutoSize = true;
        lblForegroundColor.Location = new Point(518, 14);
        lblForegroundColor.Name = "lblForegroundColor";
        lblForegroundColor.Size = new Size(56, 20);
        lblForegroundColor.TabIndex = 4;
        lblForegroundColor.Text = "Código";
        // 
        // numModuleSize
        // 
        numModuleSize.Location = new Point(392, 37);
        numModuleSize.Name = "numModuleSize";
        numModuleSize.Size = new Size(108, 27);
        numModuleSize.TabIndex = 3;
        numModuleSize.ValueChanged += options_ValueChanged;
        // 
        // lblModuleSize
        // 
        lblModuleSize.AutoSize = true;
        lblModuleSize.Location = new Point(277, 14);
        lblModuleSize.Name = "lblModuleSize";
        lblModuleSize.Size = new Size(223, 20);
        lblModuleSize.TabIndex = 2;
        lblModuleSize.Text = "Tamaño (píxeles por módulo)";
        // 
        // cmbErrorCorrection
        // 
        cmbErrorCorrection.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbErrorCorrection.FormattingEnabled = true;
        cmbErrorCorrection.Location = new Point(18, 36);
        cmbErrorCorrection.Name = "cmbErrorCorrection";
        cmbErrorCorrection.Size = new Size(240, 28);
        cmbErrorCorrection.TabIndex = 1;
        cmbErrorCorrection.Format += cmbErrorCorrection_Format;
        cmbErrorCorrection.SelectedValueChanged += options_ValueChanged;
        // 
        // lblErrorCorrection
        // 
        lblErrorCorrection.AutoSize = true;
        lblErrorCorrection.Location = new Point(18, 14);
        lblErrorCorrection.Name = "lblErrorCorrection";
        lblErrorCorrection.Size = new Size(132, 20);
        lblErrorCorrection.TabIndex = 0;
        lblErrorCorrection.Text = "Corrección de error";
        // 
        // btnGenerate
        // 
        btnGenerate.Location = new Point(12, 250);
        btnGenerate.Name = "btnGenerate";
        btnGenerate.Size = new Size(130, 34);
        btnGenerate.TabIndex = 4;
        btnGenerate.Text = "Generar QR";
        btnGenerate.UseVisualStyleBackColor = true;
        btnGenerate.Click += btnGenerate_Click;
        // 
        // btnSaveAs
        // 
        btnSaveAs.Enabled = false;
        btnSaveAs.Location = new Point(148, 250);
        btnSaveAs.Name = "btnSaveAs";
        btnSaveAs.Size = new Size(130, 34);
        btnSaveAs.TabIndex = 5;
        btnSaveAs.Text = "Guardar como…";
        btnSaveAs.UseVisualStyleBackColor = true;
        btnSaveAs.Click += btnSaveAs_Click;
        // 
        // btnCopy
        // 
        btnCopy.Enabled = false;
        btnCopy.Location = new Point(284, 250);
        btnCopy.Name = "btnCopy";
        btnCopy.Size = new Size(130, 34);
        btnCopy.TabIndex = 6;
        btnCopy.Text = "Copiar";
        btnCopy.UseVisualStyleBackColor = true;
        btnCopy.Click += btnCopy_Click;
        // 
        // imgQR
        // 
        imgQR.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        imgQR.BackColor = Color.White;
        imgQR.BorderStyle = BorderStyle.FixedSingle;
        imgQR.Location = new Point(12, 296);
        imgQR.Name = "imgQR";
        imgQR.Size = new Size(896, 302);
        imgQR.SizeMode = PictureBoxSizeMode.Zoom;
        imgQR.TabIndex = 7;
        imgQR.TabStop = false;
        // 
        // lblStatus
        // 
        lblStatus.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblStatus.AutoEllipsis = true;
        lblStatus.Location = new Point(12, 609);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(896, 28);
        lblStatus.TabIndex = 8;
        lblStatus.Text = "Introduce un texto y pulsa Generar QR.";
        // 
        // Form1
        // 
        AcceptButton = btnGenerate;
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(920, 646);
        Controls.Add(lblStatus);
        Controls.Add(imgQR);
        Controls.Add(btnCopy);
        Controls.Add(btnSaveAs);
        Controls.Add(btnGenerate);
        Controls.Add(grpOptions);
        Controls.Add(lblPayloadSize);
        Controls.Add(txtCode);
        Controls.Add(lblInstructions);
        MinimumSize = new Size(760, 600);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "FRMS QR Generator";
        grpOptions.ResumeLayout(false);
        grpOptions.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numModuleSize).EndInit();
        ((System.ComponentModel.ISupportInitialize)imgQR).EndInit();
        ResumeLayout(false);
        PerformLayout();
    }

    private Label lblInstructions = null!;
    private TextBox txtCode = null!;
    private Label lblPayloadSize = null!;
    private GroupBox grpOptions = null!;
    private ComboBox cmbErrorCorrection = null!;
    private Label lblErrorCorrection = null!;
    private NumericUpDown numModuleSize = null!;
    private Label lblModuleSize = null!;
    private Button btnForegroundColor = null!;
    private Label lblForegroundColor = null!;
    private Button btnBackgroundColor = null!;
    private Label lblBackgroundColor = null!;
    private Button btnGenerate = null!;
    private Button btnSaveAs = null!;
    private Button btnCopy = null!;
    private PictureBox imgQR = null!;
    private Label lblStatus = null!;
}
