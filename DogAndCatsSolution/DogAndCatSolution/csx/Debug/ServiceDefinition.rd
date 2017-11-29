<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="HapoalimSolution" generation="1" functional="0" release="0" Id="9c701284-9b2f-498f-a956-598b37fc5ad6" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="HapoalimSolutionGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="DogAndCatAPI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/HapoalimSolution/HapoalimSolutionGroup/LB:DogAndCatAPI:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="DogAndCatAPI:AllowedDomains" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:AllowedDomains" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:APPINSIGHTS_INSTRUMENTATIONKEY" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Cat_EndDate" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Cat_EndDate" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Cat_ServerToken" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Cat_ServerToken" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Cat_StartDate" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Cat_StartDate" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:ClientFetchDataInterval_AsMinutes" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:ClientFetchDataInterval_AsMinutes" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:ConfigUpdateAsMinutes" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:ConfigUpdateAsMinutes" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Dog_EndDate" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Dog_EndDate" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Dog_ServerToken" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Dog_ServerToken" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Dog_StartDate" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Dog_StartDate" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:IP_AllowedPerCycle" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:IP_AllowedPerCycle" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:IP_MinuteCycle" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:IP_MinuteCycle" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Local_Cache_Refresh_Hour_Interval" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Local_Cache_Refresh_Hour_Interval" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:LogAllRequest" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:LogAllRequest" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:RenewProjectToken" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:RenewProjectToken" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:RestrictServerIP" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:RestrictServerIP" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Unique_AllowedPerCycle" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Unique_AllowedPerCycle" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:Unique_MinuteCycle" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:Unique_MinuteCycle" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPI:VERSION" defaultValue="">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPI:VERSION" />
          </maps>
        </aCS>
        <aCS name="DogAndCatAPIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/HapoalimSolution/HapoalimSolutionGroup/MapDogAndCatAPIInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:DogAndCatAPI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapDogAndCatAPI:AllowedDomains" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/AllowedDomains" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:APPINSIGHTS_INSTRUMENTATIONKEY" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/APPINSIGHTS_INSTRUMENTATIONKEY" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Cat_EndDate" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Cat_EndDate" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Cat_ServerToken" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Cat_ServerToken" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Cat_StartDate" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Cat_StartDate" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:ClientFetchDataInterval_AsMinutes" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/ClientFetchDataInterval_AsMinutes" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:ConfigUpdateAsMinutes" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/ConfigUpdateAsMinutes" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Dog_EndDate" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Dog_EndDate" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Dog_ServerToken" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Dog_ServerToken" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Dog_StartDate" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Dog_StartDate" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:IP_AllowedPerCycle" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/IP_AllowedPerCycle" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:IP_MinuteCycle" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/IP_MinuteCycle" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Local_Cache_Refresh_Hour_Interval" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Local_Cache_Refresh_Hour_Interval" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:LogAllRequest" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/LogAllRequest" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:RenewProjectToken" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/RenewProjectToken" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:RestrictServerIP" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/RestrictServerIP" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Unique_AllowedPerCycle" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Unique_AllowedPerCycle" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:Unique_MinuteCycle" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/Unique_MinuteCycle" />
          </setting>
        </map>
        <map name="MapDogAndCatAPI:VERSION" kind="Identity">
          <setting>
            <aCSMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI/VERSION" />
          </setting>
        </map>
        <map name="MapDogAndCatAPIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPIInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="DogAndCatAPI" generation="1" functional="0" release="0" software="C:\Users\sharonb\Documents\GitHub\DogAndCatPauchServer\DogAndCatServer\DogAndCatsSolution\DogAndCatSolution\csx\Debug\roles\DogAndCatAPI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="-1" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="AllowedDomains" defaultValue="" />
              <aCS name="APPINSIGHTS_INSTRUMENTATIONKEY" defaultValue="" />
              <aCS name="Cat_EndDate" defaultValue="" />
              <aCS name="Cat_ServerToken" defaultValue="" />
              <aCS name="Cat_StartDate" defaultValue="" />
              <aCS name="ClientFetchDataInterval_AsMinutes" defaultValue="" />
              <aCS name="ConfigUpdateAsMinutes" defaultValue="" />
              <aCS name="Dog_EndDate" defaultValue="" />
              <aCS name="Dog_ServerToken" defaultValue="" />
              <aCS name="Dog_StartDate" defaultValue="" />
              <aCS name="IP_AllowedPerCycle" defaultValue="" />
              <aCS name="IP_MinuteCycle" defaultValue="" />
              <aCS name="Local_Cache_Refresh_Hour_Interval" defaultValue="" />
              <aCS name="LogAllRequest" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="RenewProjectToken" defaultValue="" />
              <aCS name="RestrictServerIP" defaultValue="" />
              <aCS name="Unique_AllowedPerCycle" defaultValue="" />
              <aCS name="Unique_MinuteCycle" defaultValue="" />
              <aCS name="VERSION" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;DogAndCatAPI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;DogAndCatAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="DogAndCatAPIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="DogAndCatAPIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="DogAndCatAPIInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="7bf6d485-db8b-45de-bbe6-067f9b22f1b8" ref="Microsoft.RedDog.Contract\ServiceContract\HapoalimSolutionContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="a89a9065-d358-4a7d-b062-16081ec22f3b" ref="Microsoft.RedDog.Contract\Interface\DogAndCatAPI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/HapoalimSolution/HapoalimSolutionGroup/DogAndCatAPI:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>