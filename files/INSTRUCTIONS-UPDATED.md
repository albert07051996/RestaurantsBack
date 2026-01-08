# 🎯 ᲡᲬᲝᲠᲘ DOCKERFILE-ები შენი პროექტისთვის

## ✅ რა ვიპოვეთ:
- **არ არის** ApiGateway პროექტი
- **არ არის** Order Service პროექტი  
- **არის მხოლოდ:** Identity და Product services
- Shared პროექტი ეწოდება **BuildingBlocks.Shared.csproj**

---

## 🚀 **რეკომენდაცია: დასაწყისისთვის მხოლოდ Identity Service**

### ნაბიჯი 1: ჩაანაცვლე Dockerfile

```powershell
# წაშალე ძველი Dockerfile
Remove-Item Dockerfile -ErrorAction SilentlyContinue

# გადაიტანე ახალი (გადაარქვი Dockerfile.new → Dockerfile)
# ან უბრალოდ გადააკოპირე Dockerfile.new-ის შიგთავსი Dockerfile-ში
```

**Dockerfile.new გამოიყენე როგორც `Dockerfile`** (root-ში)

---

### ნაბიჯი 2: Git-ში ატვირთვა

```powershell
# დაამატე ახალი Dockerfile
git add Dockerfile
git commit -m "Add correct Dockerfile for Identity service"
git push origin main
```

---

### ნაბიჯი 3: Render Dashboard

1. გადადი: https://dashboard.render.com
2. **New +** → **Web Service**
3. აირჩიე repository: `albert07051996/RestaurantsBack`
4. კონფიგურაცია:
   - **Name**: `restaurants-identity`
   - **Runtime**: Docker
   - **Dockerfile Path**: `./Dockerfile`
   - **Instance Type**: Free
5. **Environment Variables** დაამატე:
   ```
   ASPNETCORE_ENVIRONMENT=Production
   ASPNETCORE_URLS=http://+:10000
   ```
6. დააწკაპუნე **Create Web Service**

---

## 🏗️ **თუ გინდა ორივე სერვისის Deploy (Identity + Product):**

### ფაილები რომლებიც გჭირდება:

1. **Dockerfile.identity** → გადაარქვი → `Dockerfile.identity`
2. **Dockerfile.product** → გადაარქვი → `Dockerfile.product`  
3. **render.yaml.new** → გადაარქვი → `render.yaml`

```powershell
# root დირექტორიაში:
git add Dockerfile.identity Dockerfile.product render.yaml
git commit -m "Add Dockerfiles for all services"
git push origin main
```

შემდეგ **Render Dashboard** → **New +** → **Blueprint** → აირჩიე repository

---

## 📦 **Database კონფიგურაცია:**

Render Dashboard-ზე:
1. **New +** → **PostgreSQL**
2. **Name**: `restaurants-postgres`
3. **Database**: `restaurantsdb`
4. **Plan**: Free
5. **Create Database**

შემდეგ დააკოპირე **Internal Database URL** და დაამატე Environment Variable-ში:
```
ConnectionStrings__IdentityDb=<Internal-Database-URL>
```

---

## 🎯 **რეკომენდაცია დასაწყისისთვის:**

**გამოიყენე Dockerfile.new როგორც მთავარი Dockerfile** და deploy-ი გააკეთე მხოლოდ Identity Service-ის. როდესაც ეს იმუშავებს, შემდეგ დაამატე Product Service.

---

## ⚡ სწრაფი ვარიანტი:

```powershell
# 1. გადააკოპირე Dockerfile.new-ის შიგთავსი ახალ Dockerfile-ში
notepad Dockerfile

# 2. ჩააკოპირე Dockerfile.new-დან ყველაფერი

# 3. შეინახე და:
git add Dockerfile
git commit -m "Add Identity service Dockerfile"  
git push origin main

# 4. გადადი Render.com და შექმენი Web Service
```

---

რომელი გზით წახვალ? 🚀
