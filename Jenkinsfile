pipeline {
    agent any
    
    stages {
        stage('Build') {
            steps {
                echo 'building...'
                sh 'dotnet build secret-friend-api/'
            }
        }
        
        stage('Test') {
            steps {
                echo 'testing...'
                sh 'dotnet test secret-friend-api/'
            }
        }
        
        stage('Deploy') {
            steps {
                echo 'Building Docker image...'
                sh 'docker build -t joaomatheusfonseca/secret-friend-api:lastest .'

                echo 'Logging in to Docker Hub...'
                sh 'docker login -u joaomatheusfonseca -p fatec182unicamp'

                echo 'Pushing Docker image to Docker Hub...'
                sh 'docker push joaomatheusfonseca/friend:lastest'
            }
        }
    }
}
