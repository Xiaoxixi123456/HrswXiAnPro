﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace PcdmisServiceClientTest.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PCDRequest", Namespace="http://schemas.datacontract.org/2004/07/Hrsw.XiAnPro.PCDmisServiceContracts")]
    [System.SerializableAttribute()]
    public partial class PCDRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private PcdmisServiceClientTest.ServiceReference1.Part PartField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public PcdmisServiceClientTest.ServiceReference1.Part Part {
            get {
                return this.PartField;
            }
            set {
                if ((object.ReferenceEquals(this.PartField, value) != true)) {
                    this.PartField = value;
                    this.RaisePropertyChanged("Part");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Part", Namespace="http://schemas.datacontract.org/2004/07/Hrsw.XiAnPro.Models")]
    [System.SerializableAttribute()]
    public partial class Part : PcdmisServiceClientTest.ServiceReference1.BindableBase {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CategoryField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int CmmNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DrawingNoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int FlagField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string NameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PassField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SlotNbField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private PcdmisServiceClientTest.ServiceReference1.PartStatus StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TrayIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double XOffsetField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double YOffsetField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Category {
            get {
                return this.CategoryField;
            }
            set {
                if ((this.CategoryField.Equals(value) != true)) {
                    this.CategoryField = value;
                    this.RaisePropertyChanged("Category");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int CmmNo {
            get {
                return this.CmmNoField;
            }
            set {
                if ((this.CmmNoField.Equals(value) != true)) {
                    this.CmmNoField = value;
                    this.RaisePropertyChanged("CmmNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string DrawingNo {
            get {
                return this.DrawingNoField;
            }
            set {
                if ((object.ReferenceEquals(this.DrawingNoField, value) != true)) {
                    this.DrawingNoField = value;
                    this.RaisePropertyChanged("DrawingNo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Flag {
            get {
                return this.FlagField;
            }
            set {
                if ((this.FlagField.Equals(value) != true)) {
                    this.FlagField = value;
                    this.RaisePropertyChanged("Flag");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Name {
            get {
                return this.NameField;
            }
            set {
                if ((object.ReferenceEquals(this.NameField, value) != true)) {
                    this.NameField = value;
                    this.RaisePropertyChanged("Name");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Pass {
            get {
                return this.PassField;
            }
            set {
                if ((this.PassField.Equals(value) != true)) {
                    this.PassField = value;
                    this.RaisePropertyChanged("Pass");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int SlotNb {
            get {
                return this.SlotNbField;
            }
            set {
                if ((this.SlotNbField.Equals(value) != true)) {
                    this.SlotNbField = value;
                    this.RaisePropertyChanged("SlotNb");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public PcdmisServiceClientTest.ServiceReference1.PartStatus Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TrayId {
            get {
                return this.TrayIdField;
            }
            set {
                if ((this.TrayIdField.Equals(value) != true)) {
                    this.TrayIdField = value;
                    this.RaisePropertyChanged("TrayId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double XOffset {
            get {
                return this.XOffsetField;
            }
            set {
                if ((this.XOffsetField.Equals(value) != true)) {
                    this.XOffsetField = value;
                    this.RaisePropertyChanged("XOffset");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double YOffset {
            get {
                return this.YOffsetField;
            }
            set {
                if ((this.YOffsetField.Equals(value) != true)) {
                    this.YOffsetField = value;
                    this.RaisePropertyChanged("YOffset");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BindableBase", Namespace="http://schemas.datacontract.org/2004/07/Prism.Mvvm")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(PcdmisServiceClientTest.ServiceReference1.Part))]
    public partial class BindableBase : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PartStatus", Namespace="http://schemas.datacontract.org/2004/07/Hrsw.XiAnPro.Models")]
    public enum PartStatus : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PS_Empty = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PS_Idle = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PS_Wait = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PS_Measuring = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PS_Measured = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        PS_Error = 5,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PCDResponse", Namespace="http://schemas.datacontract.org/2004/07/Hrsw.XiAnPro.PCDmisServiceContracts")]
    [System.SerializableAttribute()]
    public partial class PCDResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsNextField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool PassField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ReportFileField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsNext {
            get {
                return this.IsNextField;
            }
            set {
                if ((this.IsNextField.Equals(value) != true)) {
                    this.IsNextField = value;
                    this.RaisePropertyChanged("IsNext");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Pass {
            get {
                return this.PassField;
            }
            set {
                if ((this.PassField.Equals(value) != true)) {
                    this.PassField = value;
                    this.RaisePropertyChanged("Pass");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ReportFile {
            get {
                return this.ReportFileField;
            }
            set {
                if ((object.ReferenceEquals(this.ReportFileField, value) != true)) {
                    this.ReportFileField = value;
                    this.RaisePropertyChanged("ReportFile");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Success {
            get {
                return this.SuccessField;
            }
            set {
                if ((this.SuccessField.Equals(value) != true)) {
                    this.SuccessField = value;
                    this.RaisePropertyChanged("Success");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PCDMessage", Namespace="http://schemas.datacontract.org/2004/07/Hrsw.XiAnPro.PCDmisServiceContracts")]
    [System.SerializableAttribute()]
    public partial class PCDMessage : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IPCDmisService", CallbackContract=typeof(PcdmisServiceClientTest.ServiceReference1.IPCDmisServiceCallback))]
    public interface IPCDmisService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/MeasurePart", ReplyAction="http://tempuri.org/IPCDmisService/MeasurePartResponse")]
        PcdmisServiceClientTest.ServiceReference1.PCDResponse MeasurePart(PcdmisServiceClientTest.ServiceReference1.PCDRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/MeasurePart", ReplyAction="http://tempuri.org/IPCDmisService/MeasurePartResponse")]
        System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> MeasurePartAsync(PcdmisServiceClientTest.ServiceReference1.PCDRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/GotoSafePostion", ReplyAction="http://tempuri.org/IPCDmisService/GotoSafePostionResponse")]
        PcdmisServiceClientTest.ServiceReference1.PCDResponse GotoSafePostion();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/GotoSafePostion", ReplyAction="http://tempuri.org/IPCDmisService/GotoSafePostionResponse")]
        System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> GotoSafePostionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Connect", ReplyAction="http://tempuri.org/IPCDmisService/ConnectResponse")]
        PcdmisServiceClientTest.ServiceReference1.PCDResponse Connect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Connect", ReplyAction="http://tempuri.org/IPCDmisService/ConnectResponse")]
        System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> ConnectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Disconnect", ReplyAction="http://tempuri.org/IPCDmisService/DisconnectResponse")]
        PcdmisServiceClientTest.ServiceReference1.PCDResponse Disconnect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Disconnect", ReplyAction="http://tempuri.org/IPCDmisService/DisconnectResponse")]
        System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> DisconnectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Next", ReplyAction="http://tempuri.org/IPCDmisService/NextResponse")]
        void Next();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Next", ReplyAction="http://tempuri.org/IPCDmisService/NextResponse")]
        System.Threading.Tasks.Task NextAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Retry", ReplyAction="http://tempuri.org/IPCDmisService/RetryResponse")]
        void Retry();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Retry", ReplyAction="http://tempuri.org/IPCDmisService/RetryResponse")]
        System.Threading.Tasks.Task RetryAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPCDmisServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/SendMessage", ReplyAction="http://tempuri.org/IPCDmisService/SendMessageResponse")]
        void SendMessage(PcdmisServiceClientTest.ServiceReference1.PCDMessage response);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPCDmisServiceChannel : PcdmisServiceClientTest.ServiceReference1.IPCDmisService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PCDmisServiceClient : System.ServiceModel.DuplexClientBase<PcdmisServiceClientTest.ServiceReference1.IPCDmisService>, PcdmisServiceClientTest.ServiceReference1.IPCDmisService {
        
        public PCDmisServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public PCDmisServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public PCDmisServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PCDmisServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public PCDmisServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public PcdmisServiceClientTest.ServiceReference1.PCDResponse MeasurePart(PcdmisServiceClientTest.ServiceReference1.PCDRequest request) {
            return base.Channel.MeasurePart(request);
        }
        
        public System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> MeasurePartAsync(PcdmisServiceClientTest.ServiceReference1.PCDRequest request) {
            return base.Channel.MeasurePartAsync(request);
        }
        
        public PcdmisServiceClientTest.ServiceReference1.PCDResponse GotoSafePostion() {
            return base.Channel.GotoSafePostion();
        }
        
        public System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> GotoSafePostionAsync() {
            return base.Channel.GotoSafePostionAsync();
        }
        
        public PcdmisServiceClientTest.ServiceReference1.PCDResponse Connect() {
            return base.Channel.Connect();
        }
        
        public System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> ConnectAsync() {
            return base.Channel.ConnectAsync();
        }
        
        public PcdmisServiceClientTest.ServiceReference1.PCDResponse Disconnect() {
            return base.Channel.Disconnect();
        }
        
        public System.Threading.Tasks.Task<PcdmisServiceClientTest.ServiceReference1.PCDResponse> DisconnectAsync() {
            return base.Channel.DisconnectAsync();
        }
        
        public void Next() {
            base.Channel.Next();
        }
        
        public System.Threading.Tasks.Task NextAsync() {
            return base.Channel.NextAsync();
        }
        
        public void Retry() {
            base.Channel.Retry();
        }
        
        public System.Threading.Tasks.Task RetryAsync() {
            return base.Channel.RetryAsync();
        }
    }
}