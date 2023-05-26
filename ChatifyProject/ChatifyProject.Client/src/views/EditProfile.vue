<template>
  <div>
    <h1>Edit Profile</h1>
    <form @submit.prevent="submitForm">
      <div>
        <label for="name">Name:</label>
        <input type="text" id="name" v-model="name" required>
      </div>
      <div>
        <label for="email">Email:</label>
        <input type="email" id="email" v-model="email" required>
      </div>
      <div>
        <label for="bio">Bio:</label>
        <textarea id="bio" v-model="bio"></textarea>
      </div>
      <div>
        <label for="visibility">Visibility:</label>
        <select id="visibility" v-model="visibility">
          <option value="class">Class</option>
          <option value="everyone">Everyone</option>
        </select>
      </div>
      <button type="submit">Save</button>
    </form>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      name: '',
      email: '',
      bio: '',
      visibility: 'class'
    }
  },
  mounted() {
    // retrieve user data from API and populate form fields
    axios.get('/api/user')
      .then(response => {
        this.name = response.data.name;
        this.email = response.data.email;
        this.bio = response.data.bio;
        this.visibility = response.data.visibility;
      })
      .catch(error => {
        console.log(error);
      });
  },
  methods: {
    submitForm() {
      // send updated user data to API
      axios.put('/api/user', {
        name: this.name,
        email: this.email,
        bio: this.bio,
        visibility: this.visibility
      })
        .then(response => {
          console.log(response);
          // redirect to user profile page
          this.$router.push('/profile');
        })
        .catch(error => {
          console.log(error);
        });
    }
  }
}
</script>

