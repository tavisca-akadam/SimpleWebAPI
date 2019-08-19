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
            
            string( name: 'CONTAINER_NAME',
                    defaultValue: 'simplewebapicontainer',
                    description: '')
            
            string( name: 'HOST_PORT',
                    defaultValue: '57801',
                    description: '')
            
            string( name: 'CONTAINER_PORT',
                    defaultValue: '57801',
                    description: '')

            string( name: 'DOCKER_REPOSITORY',
                    defaultValue: 'images',
                    description: '')
            
            string( name: 'DOCKER_HUB_USER',
                    defaultValue: 'anilkadam',
                    description: '')
            
            string( name: 'PASSWORD',
                    defaultValue: '**********',
                    description: '')
    }
	
    stages {
        stage('Build') {
            steps {
				sh "dotnet restore ${SOLUTION_FILE_PATH} --source https://api.nuget.org/v3/index.json"
                sh "dotnet build  ${SOLUTION_FILE_PATH} -p:Configuration=release -v:n"
            }
        }
        stage('Test') {
            steps {
                sh "dotnet test ${TEST_PROJECT_PATH}"
            }
        }
        stage('SonarQube') {
            steps {
                sh 'echo SonarQube Started'
                sh 'dotnet ${Sonarqube-MSBuild} begin /k:"web_api" /d:sonar.host.url="http://localhost:9000" /d:sonar.login="7525dc0078ec1fc74c53024cb327a998eb70c455"'
                sh 'dotnet build  ${SOLUTION_FILE_PATH}'
                sh 'dotnet ${Sonarqube-MSBuild} end /d:sonar.login="7525dc0078ec1fc74c53024cb327a998eb70c455"'
            }
        }
        stage('Publish') {
            steps {
                sh "dotnet publish ${SOLUTION_FILE_PATH} -o:publish -v:q"
            }
        }
        stage('Deploy') {
            steps {
                sh 'echo in deploy'
                sh '''
                    if(docker inspect -f {{.State.Running}} ${CONTAINER_NAME})
                    then

                            docker container rm -f ${CONTAINER_NAME}

                    fi
                '''
                sh "docker build -t ${IMAGE_NAME} -f Dockerfile ."
                sh "docker run --name ${CONTAINER_NAME} -d -p ${HOST_PORT}:${CONTAINER_PORT} ${IMAGE_NAME}:latest"
                sh "docker tag ${IMAGE_NAME} ${DOCKER_HUB_USER}/${DOCKER_REPOSITORY}:latest"
                withCredentials([string(credentialsId: 'docker-pwd', variable: 'DockerHubPassword')]) {
                    sh "docker login -u ${DOCKER_HUB_USER} -p ${DockerHubPassword}"
                }
                
                sh "docker push ${DOCKER_HUB_USER}/${DOCKER_REPOSITORY}:latest"
                sh "docker image rm -f ${IMAGE_NAME}:latest"
            }
        }
    }
    post {
        always {
            deleteDir()
        }
    }
}