﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hrsw.XiAnPro.CMMClients.PcdmisServiceReference {
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
        private Hrsw.XiAnPro.Models.Part PartField;
        
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
        public Hrsw.XiAnPro.Models.Part Part {
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
    [System.Runtime.Serialization.DataContractAttribute(Name="PCDResponse", Namespace="http://schemas.datacontract.org/2004/07/Hrsw.XiAnPro.PCDmisServiceContracts")]
    [System.SerializableAttribute()]
    public partial class PCDResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
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
        private bool ErrorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool OperTypeField;
        
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
        public bool Error {
            get {
                return this.ErrorField;
            }
            set {
                if ((this.ErrorField.Equals(value) != true)) {
                    this.ErrorField = value;
                    this.RaisePropertyChanged("Error");
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
        public bool OperType {
            get {
                return this.OperTypeField;
            }
            set {
                if ((this.OperTypeField.Equals(value) != true)) {
                    this.OperTypeField = value;
                    this.RaisePropertyChanged("OperType");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PcdmisServiceReference.IPCDmisService", CallbackContract=typeof(Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.IPCDmisServiceCallback))]
    public interface IPCDmisService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/MeasurePart", ReplyAction="http://tempuri.org/IPCDmisService/MeasurePartResponse")]
        Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse MeasurePart(Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/MeasurePart", ReplyAction="http://tempuri.org/IPCDmisService/MeasurePartResponse")]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> MeasurePartAsync(Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/ReleaseMeasure", ReplyAction="http://tempuri.org/IPCDmisService/ReleaseMeasureResponse")]
        void ReleaseMeasure();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/ReleaseMeasure", ReplyAction="http://tempuri.org/IPCDmisService/ReleaseMeasureResponse")]
        System.Threading.Tasks.Task ReleaseMeasureAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/GotoSafePostion", ReplyAction="http://tempuri.org/IPCDmisService/GotoSafePostionResponse")]
        Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse GotoSafePostion();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/GotoSafePostion", ReplyAction="http://tempuri.org/IPCDmisService/GotoSafePostionResponse")]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> GotoSafePostionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Connect", ReplyAction="http://tempuri.org/IPCDmisService/ConnectResponse")]
        Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse Connect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Connect", ReplyAction="http://tempuri.org/IPCDmisService/ConnectResponse")]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> ConnectAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Disconnect", ReplyAction="http://tempuri.org/IPCDmisService/DisconnectResponse")]
        Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse Disconnect();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/Disconnect", ReplyAction="http://tempuri.org/IPCDmisService/DisconnectResponse")]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> DisconnectAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPCDmisServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPCDmisService/SendMessage", ReplyAction="http://tempuri.org/IPCDmisService/SendMessageResponse")]
        void SendMessage(Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDMessage response);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPCDmisServiceChannel : Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.IPCDmisService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PCDmisServiceClient : System.ServiceModel.DuplexClientBase<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.IPCDmisService>, Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.IPCDmisService {
        
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
        
        public Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse MeasurePart(Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDRequest request) {
            return base.Channel.MeasurePart(request);
        }
        
        public System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> MeasurePartAsync(Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDRequest request) {
            return base.Channel.MeasurePartAsync(request);
        }
        
        public void ReleaseMeasure() {
            base.Channel.ReleaseMeasure();
        }
        
        public System.Threading.Tasks.Task ReleaseMeasureAsync() {
            return base.Channel.ReleaseMeasureAsync();
        }
        
        public Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse GotoSafePostion() {
            return base.Channel.GotoSafePostion();
        }
        
        public System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> GotoSafePostionAsync() {
            return base.Channel.GotoSafePostionAsync();
        }
        
        public Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse Connect() {
            return base.Channel.Connect();
        }
        
        public System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> ConnectAsync() {
            return base.Channel.ConnectAsync();
        }
        
        public Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse Disconnect() {
            return base.Channel.Disconnect();
        }
        
        public System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.PcdmisServiceReference.PCDResponse> DisconnectAsync() {
            return base.Channel.DisconnectAsync();
        }
    }
}
