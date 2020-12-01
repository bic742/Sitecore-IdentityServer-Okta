# Introduction 
This repository contains the configuration and code necessary for Sitecore Identity Server to authenticate a CE user via Okta. 

# Getting Started
Deployment structure:

```
.
├── sitecoreruntime
|   ├── production
|   |   ├── sitecore
|   |   |   ├── Sitecore.Plugin.IdentityProviders.Okta
|   |   |   |   ├── Config
|   |   |   |   |   ├── Sitecore.Plugin.IdentityProvider.Okta.xml
|   |   |   |   └── Sitecore.Plugin.manifest
|   |	├── Okta.FederatedAuth.dll
|   |	└── Okta.FederatedAuth.pdb
|   └── license.xml
```

# Okta Setup
Check out this blog post for details on how to configure Okta: https://www.xcentium.com/blog/2020/04/14/federated-auth-via-okta
