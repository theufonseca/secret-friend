pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                sh 'dotnet build secret-friend-api/'
            }
        }
        
        stage('Test') {
            steps {
                sh 'dotnet test secret-friend-api/'
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'Deploying The Project....'
            }
        }
    }
}
