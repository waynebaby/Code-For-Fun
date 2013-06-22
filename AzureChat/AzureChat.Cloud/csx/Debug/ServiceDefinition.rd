<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="AzureChat.Cloud" generation="1" functional="0" release="0" Id="a2df0182-eef0-4b48-895e-2c70dc4befc8" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="AzureChat.CloudGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="AzureChat.Hosts.WebHost:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/LB:AzureChat.Hosts.WebHost:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="AzureChat.Hosts.ChatroomHost:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/MapAzureChat.Hosts.ChatroomHost:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AzureChat.Hosts.ChatroomHostInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/MapAzureChat.Hosts.ChatroomHostInstances" />
          </maps>
        </aCS>
        <aCS name="AzureChat.Hosts.WebHost:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/MapAzureChat.Hosts.WebHost:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="AzureChat.Hosts.WebHostInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/MapAzureChat.Hosts.WebHostInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:AzureChat.Hosts.WebHost:Endpoint1">
          <toPorts>
            <inPortMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHost/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapAzureChat.Hosts.ChatroomHost:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.ChatroomHost/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAzureChat.Hosts.ChatroomHostInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.ChatroomHostInstances" />
          </setting>
        </map>
        <map name="MapAzureChat.Hosts.WebHost:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHost/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapAzureChat.Hosts.WebHostInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHostInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="AzureChat.Hosts.ChatroomHost" generation="1" functional="0" release="0" software="C:\Users\waywa\documents\visual studio 2012\Projects\AzureChat\AzureChat.Cloud\csx\Debug\roles\AzureChat.Hosts.ChatroomHost" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AzureChat.Hosts.ChatroomHost&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AzureChat.Hosts.ChatroomHost&quot; /&gt;&lt;r name=&quot;AzureChat.Hosts.WebHost&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.ChatroomHostInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.ChatroomHostUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.ChatroomHostFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="AzureChat.Hosts.WebHost" generation="1" functional="0" release="0" software="C:\Users\waywa\documents\visual studio 2012\Projects\AzureChat\AzureChat.Cloud\csx\Debug\roles\AzureChat.Hosts.WebHost" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;AzureChat.Hosts.WebHost&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;AzureChat.Hosts.ChatroomHost&quot; /&gt;&lt;r name=&quot;AzureChat.Hosts.WebHost&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHostInstances" />
            <sCSPolicyUpdateDomainMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHostUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHostFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="AzureChat.Hosts.WebHostUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="AzureChat.Hosts.ChatroomHostUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="AzureChat.Hosts.ChatroomHostFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="AzureChat.Hosts.WebHostFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="AzureChat.Hosts.ChatroomHostInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="AzureChat.Hosts.WebHostInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="493aa02d-92e2-4933-bcd8-220c90e775e2" ref="Microsoft.RedDog.Contract\ServiceContract\AzureChat.CloudContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="a9bc615b-63da-45fa-a865-4afe35d4c6e3" ref="Microsoft.RedDog.Contract\Interface\AzureChat.Hosts.WebHost:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/AzureChat.Cloud/AzureChat.CloudGroup/AzureChat.Hosts.WebHost:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>