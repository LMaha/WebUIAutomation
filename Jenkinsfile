node()
{   def gitPath ="https://lmahajan@bitbucket.org/plstms/goshipuiautomation.git";
    def nuget = "C:\\nuget.exe";
    def nunt="C:\\NUnit.org\\nunit-console\\nunit3-console.exe";
    def slnPath ="GoShipUITests.sln";
    def dllPath="GoShipUI\\bin\\Debug\\GoShipUI.dll";
    def reportPath ="GoShipUI\\bin\\Debug\\ExtentReports\\";
    env.nameSpace = "uat";
    env.emailccList = "cc:lmahajan@plslogistics.com, jly@plslogistics.com";
    
    checkout scm;

    stage ('Restore Nuget') {
  
            bat label: 'Restore Nuget', script: "${nuget} restore \"${slnPath}\""
    }

    stage ('Build Solution') {
    
          try
          {
            bat label: 'build solution', script: "\"${tool 'MSBuild'}\" \"${slnPath} \""
          }
          catch(e)
          {
          }
    }

    stage ('Run Scripts') {
      
            try
            {
                  bat label: 'run scripts', script: "\"${nunt}\" ${dllPath}"
             
            }
               
            catch(e)
            {
               
            }
    }        
            
    stage ('Extent Report') {
           publishHTML(target:
             [allowMissing: false, 
             alwaysLinkToLastBuild:true,
             keepAll: true,
             reportDir: reportPath, 
             reportFiles: 'index.html',
             reportName: 'Extent Report', 
             reportTitles: 'report']
             )

           notifySuccessful();
    }
  
}

def notifySuccessful() {
    emailext (
      to: "cburris@plslogistics.com, ${env.emailccList}",
      replyTo: "cburris@plslogistics.com",
      subject: "Successful Build:${env.JOB_NAME}",
      body: """<p>Successful Build: ${env.JOB_NAME} [${env.BUILD_NUMBER}]</p>
        <p>Check console output at "<a href="${env.BUILD_URL}">${env.JOB_NAME} [${env.BUILD_NUMBER}]</a>"</p>""",
      recipientProviders: [[$class: 'DevelopersRecipientProvider']]
    )
}