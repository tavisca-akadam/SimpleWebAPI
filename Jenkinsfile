pipeline {
    agent any
	parameters {		
			string(	name: 'GIT_SSH_PATH',
					defaultValue: "https://github.com/tavisca-akadam/SimpleWebAPI.git",
					description: '')

			string(	name: 'SOLUTION_FILE_PATH',
					defaultValue: "SimpleWebAPI.sln", 
					description: '')

			string(	name: 'TEST_PROJECT_PATH',
					defaultValue: "SimpleWebAPITest/SimpleWebAPITest.csproj", 
					description: '')

            string( name: 'DEPLOY_PROJECT_PATH',
                    defaultValue: "SimpleWebAPI/publish/SimpleWebAPI.dll",
                    description: '')
            
            string( name: 'IMAGE_NAME',
                    defaultValue: 'simplewebapi',
                    description: '')
    }
	
    stages {
        stage('Build') {
            steps {
				sh 'dotnet restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json'
                sh 'dotnet build  ${SOLUTION_FILE_PATH} -p:Configuration=release -v:n'
            }
        }
        stage('Test') {
            steps {
                sh 'dotnet test ${TEST_PROJECT_PATH}' 
            }
        }
        stage('Publish') {
            steps {
                sh 'dotnet publish ${SOLUTION_FILE_PATH} -o:publish -v:q'
            }
        }
        stage('Deploy') {
            steps {
                sh 'docker build -t ${IMAGE_NAME} -f Dockerfile .'
                sh 'docker run -rm -p 57801:57801 ${IMAGE_NAME}:latest'
            }
        }
    }
}