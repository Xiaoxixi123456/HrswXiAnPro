﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hrsw.XiAnPro.CMMClients.FileServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="FileServiceReference.IFileService")]
    public interface IFileService {
        
        // CODEGEN: 消息 UpFile 的包装名称(UpFile)以后生成的消息协定与默认值(UpLoadFile)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/UpLoadFile", ReplyAction="http://tempuri.org/IFileService/UpLoadFileResponse")]
        Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFileResult UpLoadFile(Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/UpLoadFile", ReplyAction="http://tempuri.org/IFileService/UpLoadFileResponse")]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFileResult> UpLoadFileAsync(Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile request);
        
        // CODEGEN: 消息 DownFile 的包装名称(DownFile)以后生成的消息协定与默认值(DownLoadFile)不匹配
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/DownLoadFile", ReplyAction="http://tempuri.org/IFileService/DownLoadFileResponse")]
        Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFileResult DownLoadFile(Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IFileService/DownLoadFile", ReplyAction="http://tempuri.org/IFileService/DownLoadFileResponse")]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFileResult> DownLoadFileAsync(Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UpFile {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string FileName;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public long FileSize;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream FileStream;
        
        public UpFile() {
        }
        
        public UpFile(string FileName, long FileSize, System.IO.Stream FileStream) {
            this.FileName = FileName;
            this.FileSize = FileSize;
            this.FileStream = FileStream;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UpFileResult", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UpFileResult {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public bool IsSuccess;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string Message;
        
        public UpFileResult() {
        }
        
        public UpFileResult(bool IsSuccess, string Message) {
            this.IsSuccess = IsSuccess;
            this.Message = Message;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownFile", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownFile {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string FileName;
        
        public DownFile() {
        }
        
        public DownFile(string FileName) {
            this.FileName = FileName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownFileResult", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownFileResult {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public long FileSize;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public bool IsSuccess;
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public string Message;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream FileStream;
        
        public DownFileResult() {
        }
        
        public DownFileResult(long FileSize, bool IsSuccess, string Message, System.IO.Stream FileStream) {
            this.FileSize = FileSize;
            this.IsSuccess = IsSuccess;
            this.Message = Message;
            this.FileStream = FileStream;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IFileServiceChannel : Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class FileServiceClient : System.ServiceModel.ClientBase<Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService>, Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService {
        
        public FileServiceClient() {
        }
        
        public FileServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public FileServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public FileServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFileResult Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService.UpLoadFile(Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile request) {
            return base.Channel.UpLoadFile(request);
        }
        
        public bool UpLoadFile(string FileName, long FileSize, System.IO.Stream FileStream, out string Message) {
            Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile inValue = new Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile();
            inValue.FileName = FileName;
            inValue.FileSize = FileSize;
            inValue.FileStream = FileStream;
            Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFileResult retVal = ((Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService)(this)).UpLoadFile(inValue);
            Message = retVal.Message;
            return retVal.IsSuccess;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFileResult> Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService.UpLoadFileAsync(Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile request) {
            return base.Channel.UpLoadFileAsync(request);
        }
        
        public System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFileResult> UpLoadFileAsync(string FileName, long FileSize, System.IO.Stream FileStream) {
            Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile inValue = new Hrsw.XiAnPro.CMMClients.FileServiceReference.UpFile();
            inValue.FileName = FileName;
            inValue.FileSize = FileSize;
            inValue.FileStream = FileStream;
            return ((Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService)(this)).UpLoadFileAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFileResult Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService.DownLoadFile(Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile request) {
            return base.Channel.DownLoadFile(request);
        }
        
        public long DownLoadFile(string FileName, out bool IsSuccess, out string Message, out System.IO.Stream FileStream) {
            Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile inValue = new Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile();
            inValue.FileName = FileName;
            Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFileResult retVal = ((Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService)(this)).DownLoadFile(inValue);
            IsSuccess = retVal.IsSuccess;
            Message = retVal.Message;
            FileStream = retVal.FileStream;
            return retVal.FileSize;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFileResult> Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService.DownLoadFileAsync(Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile request) {
            return base.Channel.DownLoadFileAsync(request);
        }
        
        public System.Threading.Tasks.Task<Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFileResult> DownLoadFileAsync(string FileName) {
            Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile inValue = new Hrsw.XiAnPro.CMMClients.FileServiceReference.DownFile();
            inValue.FileName = FileName;
            return ((Hrsw.XiAnPro.CMMClients.FileServiceReference.IFileService)(this)).DownLoadFileAsync(inValue);
        }
    }
}