namespace SPDTProtocolTeste;

public partial class SPDTProtocolTesteForm : Form
{
    public SPDTProtocolTesteForm()
    {
        InitializeComponent();
    }

    User UserProtocol1 = new();
    User Userprotocol2 = new();
    private void SPDTProtocolTeste_Load(object sender, EventArgs e)
    {

    }

    private void Btn_CreateSPDTCore_Click(object sender, EventArgs e)
    {
        UserProtocol1.CreateProtocol(Userprotocol2);
        Userprotocol2.CreateProtocol(UserProtocol1);
    }

    private void Btn_CreateStreamOnUser1_Click(object sender, EventArgs e)
    {
        var Data = UserProtocol1.CreateStreamMessageTesteStream();
        Userprotocol2.SimulateSendMessage(Data);
    }
}