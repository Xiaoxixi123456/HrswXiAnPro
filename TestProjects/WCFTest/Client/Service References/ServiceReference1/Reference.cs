﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Client.ServiceReference1 {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="BindableBase", Namespace="http://schemas.datacontract.org/2004/07/Prism.Mvvm")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(Models.Part))]
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServiceContract", CallbackContract=typeof(Client.ServiceReference1.IServiceContractCallback))]
    public interface IServiceContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Hook", ReplyAction="http://tempuri.org/IServiceContract/HookResponse")]
        void Hook();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Hook", ReplyAction="http://tempuri.org/IServiceContract/HookResponse")]
        System.Threading.Tasks.Task HookAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Operator", ReplyAction="http://tempuri.org/IServiceContract/OperatorResponse")]
        bool Operator();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Operator", ReplyAction="http://tempuri.org/IServiceContract/OperatorResponse")]
        System.Threading.Tasks.Task<bool> OperatorAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/ReturnString", ReplyAction="http://tempuri.org/IServiceContract/ReturnStringResponse")]
        string ReturnString();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/ReturnString", ReplyAction="http://tempuri.org/IServiceContract/ReturnStringResponse")]
        System.Threading.Tasks.Task<string> ReturnStringAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Send", ReplyAction="http://tempuri.org/IServiceContract/SendResponse")]
        string Send(Contracts.OpRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Send", ReplyAction="http://tempuri.org/IServiceContract/SendResponse")]
        System.Threading.Tasks.Task<string> SendAsync(Contracts.OpRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceContractCallback {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceContract/Reponse", ReplyAction="http://tempuri.org/IServiceContract/ReponseResponse")]
        void Reponse();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceContractChannel : Client.ServiceReference1.IServiceContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceContractClient : System.ServiceModel.DuplexClientBase<Client.ServiceReference1.IServiceContract>, Client.ServiceReference1.IServiceContract {
        
        public ServiceContractClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ServiceContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ServiceContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceContractClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceContractClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public void Hook() {
            base.Channel.Hook();
        }
        
        public System.Threading.Tasks.Task HookAsync() {
            return base.Channel.HookAsync();
        }
        
        public bool Operator() {
            return base.Channel.Operator();
        }
        
        public System.Threading.Tasks.Task<bool> OperatorAsync() {
            return base.Channel.OperatorAsync();
        }
        
        public string ReturnString() {
            return base.Channel.ReturnString();
        }
        
        public System.Threading.Tasks.Task<string> ReturnStringAsync() {
            return base.Channel.ReturnStringAsync();
        }
        
        public string Send(Contracts.OpRequest request) {
            return base.Channel.Send(request);
        }
        
        public System.Threading.Tasks.Task<string> SendAsync(Contracts.OpRequest request) {
            return base.Channel.SendAsync(request);
        }
    }
}
