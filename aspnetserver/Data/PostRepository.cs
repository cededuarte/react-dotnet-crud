using System;
using Microsoft.EntityFrameworkCore;


//FETCH USERS FROM DATABASE
namespace aspnetserver.Data
{
    internal static class PostRepository
    {
        internal async static Task<List<Post>> GetPostsAsync()
        {
            using (var db = new AppDBContext())
            {
                return await db.Posts.ToListAsync();
            }
        }
        internal async static Task<Post>GetPostByIdAsync(int postId)
        {
            using(var db = new AppDBContext())
            {
                return await db.Posts.FirstOrDefaultAsync(post => post.PostId == postId);
            }

        }
        //CREATE USER
        internal async static Task<bool> CreatePostAsync(Post postToCreate)
        {
            using(var db = new AppDBContext())
            {
               try
                {
                    await db.Posts.AddAsync(postToCreate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch(Exception e)
                {
                    return false;
                }
            }
        }

        //UPDATE USER
        internal async static Task<bool> UpdatePostAsync(Post postToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                     db.Posts.Update(postToCreate);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }

        //DELETE USER
        internal async static Task<bool> DeletePostAsync(int postId)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    Post postToDelete = await GetPostByIdAsync(postId);

                    db.Remove(postToDelete);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }



    }
}

