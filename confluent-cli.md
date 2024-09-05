# Confluent CLI Documentation

## Overview
The Confluent CLI allows you to administer your Confluent Cloud cluster and components using command-line tools. This guide provides steps for installing, configuring, and using the CLI to interact with your Kafka clusters.

## 1. Install / Update the Confluent CLI
Run this command to install the Confluent CLI:
```bash
curl -sL --http1.1 https://cnfl.io/cli | sh -s -- latest
```
This script will install the CLI in ./bin by default. If you want to install it somewhere else, add the path to the end of the command and to your $PATH variable.

**Note** On Windows, you might need to install an appropriate Linux environment to have the curl and sh commands available, such as the Windows Subsystem for Linux. You can also download and install the raw binaries.

If already installed, update to the latest version with:
```bash
confluent update
```


## 2. Log in to your Confluent Cloud organization using the Confluent CLI
Run this command to log in to the Confluent CLI:
```bash
confluent login --save
```

When prompted for your username and password, enter the same credentials that you used to log in to Confluent Cloud.
The optional --save flag saves your login credentials to a local file for future use.

## 3. Select your environment
Run this command to set your environment. To view the available environments, use the **confluent environment list** command.
```bash
confluent environment use env-yyzk1j
```

## 4. Select your cluster
Run this command to set your cluster. To view the available clusters, use the **confluent kafka cluster list** command.
```bash
confluent kafka cluster use lkc-1y6qkz
```

## 5. Use an API key and secret in the CLI
An API key is required to produce or consume to your topic. If you have an existing API key that you'd like to use, add it to the CLI with this command:
```bash
confluent api-key store --resource lkc-1y6qkz
Key: <API_KEY>
Secret: <API_SECRET>
```
Otherwise, create a new API key and secret pair using this command:
```bash
confluent api-key create --resource lkc-1y6qkz
```
**Note**: Save your API key and secret pair in a safe and secure place.
After you have created or added your API key pair, copy the API key and paste it at the end of this command:
```bash
confluent api-key use <API_Key>
```

## 6. Create a topic
Run this command to create a topic named test-topic in your cluster:
```bash
confluent kafka topic create test-topic
```
Now, verify that your topic has been created:
```bash
confluent kafka topic list
```

## 7. Produce messages to your topic
Now you're ready to produce and consume some messages.
Run this command to start a console producer, which you can use to manually produce messages to test-topic
```bah
confluent kafka topic produce test-topic
```

Run that command, then produce a few messages to your cluster in Confluent Cloud using the console producer. You can do this by typing anything into the console and pressing Enter. For example:
confluent kafka topic produce test-topic
```bash
"test"
"test"
"foo"
"bar"
```
When you're done, press ctrl+C to exit the producer.

## 8. Consume the messages you produced to your topic
To consume all of the messages in test-topic and print them to the console, run this command:
```bash
confluent kafka topic consume --from-beginning test-topic
```
To use a specific Consumer Group, append the following to the command: **'--group YOUR_CONSUMER_GROUP_NAME'**. If the Consumer Group does not already exist, one will be automatically created.

**Note**: You can run the consume command in parallel with the produce command in different instances of the CLI. To try this, keep the consumer running, and open another terminal window. Then, in the new window, run the produce command from the previous step and produce some messages. You should see the messages being consumed in real time in the window where your consumer is running.

Press ctrl+C to stop the consumer.

## 9. Generate a client config
If you're ready to set up a producer or consumer, you can generate a config for the client using the CLI.
```bash
confluent kafka client-config create <LANGUAGE> --api-key <API_KEY> --api-secret <API_SECRET>
```

Supported languages:
Java*
Python*
C#
Node.js
Spring Boot*
Go
C/C++
REST API
Scala
Clojure
Ruby
Ktor*
Rust
Groovy
Learn more about creating clients here.
* These languages support Schema Registry keys. Append
**--schema-registry-api-key <SR_KEY>** and **--schema-registry-api-secret <SR_SECRET>** to the above command.
